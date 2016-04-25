var ApplicationManager = (function() {

    setMaxDigits(262);

    function doEncrypt(mValue, eValue, keySize, textToEncript) {
        key = new RSAKeyPair(eValue, eValue, mValue, keySize);

        // to encrypt phone number we use PKCS #1 v1.5 padding
        // the encrypted phone number is represented in Hex format of byte array
        return encryptedString(key,
            textToEncript,
            RSAAPP.PKCS1Padding,
            RSAAPP.NumericEncoding);
    }

    function init(config) {
        var modules = ['ui.grid', 'ui.grid.edit', 'ngSanitize', 'ui.select2'];

        var linkCellTemplate = '<div class="ngCellText" ng-class="col.colIndex()">' +
            '  <a href="' +
            config.applicationDetailsUrl +
            '?applicationId={{COL_FIELD}}">{{COL_FIELD}}</a>' +
            '</div>';

        var app = angular.module('globalApp');
        app.injectRequires(modules);

        app.service('ApplicationService',
            function() {
                this.connection = $.connection;
            });

        app.controller('applicationControl',
        [
            '$scope', 'ApplicationService', function($scope, ApplicationService) {
                // Setup hub to use with SignalR
                ApplicationService.connection.applicationsHub.client.onApplicationCreated = function(applicationId) {
                    window.location.replace(config.applicationDetailsUrl + '?applicationId=' + applicationId);
                };
                ApplicationService.connection.hub.start().done(function() {});

                // Setup model
                $scope.applicationNumber = "";
                $scope.phoneNumber = "";
                $scope.telcos = {};
                $scope.availableCodes = [];
                $scope.selectedPhoneCode = "";

                // Check Application Number
                $scope.checkIfApplicationNumberIsUniqe = function() {
                    if (this.applicationNumber) {
                        $.ajax({
                            url: config.checkApplicationNumberUrl + "?applicationNumber=" + this.applicationNumber,
                            type: 'GET',
                            dataType: 'json',
                            success: function(data) {
                                $scope.applicationNumberCorrect = data;
                                $scope.$apply();
                            },
                            async: true
                        });
                    } else {
                        $scope.applicationNumberCorrect = true;
                    }
                };
                $scope.applicationNumberCorrect = true;

                // Phone Code
                $scope.phoneCodeSelected = function(item) {
                    $scope.selectedPhoneCode = item;
                }

                // Setup grid
                $scope.gridOptions = {
                    enableSorting: true,
                    enableRowSelection: true,
                    showGroupPanel: true,
                    columnDefs: [
                        {
                            field: 'applicationId',
                            displayName: '#',
                            cellTemplate: linkCellTemplate
                        },
                        {
                            field: 'creatorFirstName',
                            displayName: config.tableHeaders.creatorFirstName
                        },
                        {
                            field: 'creatorLastName',
                            displayName: config.tableHeaders.creatorLastName
                        },
                        {
                            field: 'createdDate',
                            displayName: config.tableHeaders.createdDate
                        },
                        {
                            field: 'scoringIndex',
                            displayName: config.tableHeaders.scoringIndex
                        }
                    ]
                };
                $scope.showModal = false;
                // Create application
                $scope.onCreateApplicationClicked = function() {
                    $scope.scoringKey = '';
                    $scope.showModal = !$scope.showModal;
                };
                $scope.createNewApplication = function() {
                    var isnum = /^\d+$/.test(this.phoneNumber);
                    if (isnum) {
                        var telco;
                        for (var i = 0; i < $scope.telcos.length; i++) {
                            if ($scope.telcos[i].telcoConnectionPhoneCodes
                                .some(function (arrVal) { return arrVal.value == $scope.selectedPhoneCode }))
                            {
                                telco = $scope.telcos[i];
                            }
                        }
                        var fullPhoneNumber = $scope.selectedPhoneCode + this.phoneNumber;
                        var encriptedKey = doEncrypt(telco.certPublicKeyModulus,
                            telco.certPublicKeyExponent,
                            telco.certKeySize,
                            fullPhoneNumber);
                        ApplicationService.connection.applicationsHub.server
                            .createApplication(encriptedKey, this.applicationUniqueNumber, telco.id);
                    } else {
                        $scope.incorectCharacters = true;
                    }
                    $scope.showModal = false;
                };

                // Encryption
                $scope.encript = function() {
                    $scope.encryptedValue = doEncrypt($scope.nValue, $scope.eValue, $scope.textToEncrypt);
                }

                // Get start-up data
                $.ajax({
                    url: config.getAllUrl,
                    type: 'GET',
                    dataType: 'json',
                    contentType: "application/json",
                    success: function(data) {
                        $scope.gridOptions.data = data;
                        for (var i = 0; i < data.length; i++) {
                            $scope.gridOptions.data[i]
                                .createdDate = moment.utc($scope.gridOptions.data[i].createdDate).local().format("LLL");
                        }
                    },
                    async: false
                });
                $.ajax({
                    url: config.getTelcosUrl,
                    type: 'GET',
                    dataType: 'json',
                    success: function(data) {
                        $scope.telcos = data;
                        for (var i = 0; i < data.length; i++) {
                            var telcoCodes = data[i].telcoConnectionPhoneCodes;
                            for (var j = 0; j < telcoCodes.length; j++) {
                                $scope.availableCodes.push(telcoCodes[j].value);
                            }
                        }
                        $scope.selectedPhoneCode = $scope.availableCodes[0];
                    },
                    async: false
                });
            }
        ]);
    }
});