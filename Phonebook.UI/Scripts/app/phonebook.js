// Read a page's GET URL variables and return them as an associative array.
function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}

(function () {
    'use strict';

    var app = angular.module('PhoneBookApp', []);

    app.controller('contactController', ['$scope', '$http', contactController]);
    app.controller('contactNumberController', ['$scope', '$http', contactNumberController]);

    function contactController($scope, $http) {
        $scope.loading = true;
        $scope.UserId = getUrlVars()["UserId"];

        //get contact information 
        $http.get('/api/ContactApi/' + $scope.UserId).success(function (data) {
                $scope.contacts = data;
                $scope.loading = false;
            })
            .error(function() {
                $scope.error = "An Error has occured while loading contacts";
                $scope.loading = false;
            });

        //by pressing toggleEdit button ng-click in html, this method will be hit
        $scope.toggleEdit = function () {
            this.contact.editMode = !this.contact.editMode;
        };

        //by pressing toggleAdd button ng-click in html, this method will be hit
        $scope.toggleAdd = function () {
            $scope.addMode = !$scope.addMode;
        };

        //insert Contact
        $scope.add = function () {
            $scope.loading = true;
            this.newcontact.UserId = $scope.UserId;

            $http.post('/api/ContactApi/', this.newcontact).success(function (data) {
                $scope.addMode = false;
                $scope.contacts.push(data);
                $scope.loading = false;
            }).error(function (data) {
                $scope.error = "An error has occured while adding contact! " + data;
                $scope.loading = false;
            });
        };

        //edit Contact
        $scope.save = function () {
            $scope.loading = true;
            var con = this.contact;
            $http.put('/api/ContactApi/' + con.Id, con).success(function (data) {
                con.editMode = false;
                $scope.loading = false;
            }).error(function (data) {
                $scope.error = "An error has occured while saving contact! " + data;
                $scope.loading = false;
            });
        };

        //delete Contact
        $scope.deleteContact = function () {
            $scope.loading = true;
            var Id = this.contact.Id;
            $http.delete('/api/ContactApi/' + Id).success(function (data) {
                $.each($scope.contact, function (i) {
                    if ($scope.contacts[i].Id === Id) {
                        $scope.contacts.splice(i, 1);
                        return false;
                    }
                });
                $scope.loading = false;
            }).error(function (data) {
                $scope.error = "An error has occured while deleting contact! " + data;
                $scope.loading = false;
            });
        };
    };

    function contactNumberController($scope, $http) {
        $scope.loading = true;
        $scope.ContactId = getUrlVars()["ContactId"];

        //get contact number information 
        $http.get('/api/ContactNumberApi/' + $scope.ContactId).success(function (data) {
            $scope.contactNumbers = data;
            $scope.loading = false;
        })
        .error(function () {
            $scope.error = "An Error has occured while loading contact numbers";
            $scope.loading = false;
        });

        //by pressing toggleEdit button ng-click in html, this method will be hit
        $scope.toggleEdit = function () {
            this.contactNumber.editMode = !this.contactNumber.editMode;
        };

        //by pressing toggleAdd button ng-click in html, this method will be hit
        $scope.toggleAdd = function () {
            $scope.addMode = !$scope.addMode;
        };

        //insert Contact
        $scope.add = function () {
            $scope.loading = true;
            this.newcontactnumber.ContactId = $scope.ContactId;

            $http.post('/api/ContactNumberApi/', this.newcontactnumber).success(function (data) {
                $scope.addMode = false;
                $scope.contactNumbers.push(data);
                $scope.loading = false;
            }).error(function (data) {
                $scope.error = "An error has occured while adding contact number! " + data;
                $scope.loading = false;
            });
        };

        //edit Contact
        $scope.save = function () {
            $scope.loading = true;
            var con = this.contactNumber;
            $http.put('/api/ContactNumberApi/' + con.Id, con).success(function (data) {
                con.editMode = false;
                $scope.loading = false;
            }).error(function (data) {
                $scope.error = "An error has occured while saving contact number! " + data;
                $scope.loading = false;
            });
        };

        //delete Contact
        $scope.deleteContact = function () {
            $scope.loading = true;
            var Id = this.contactNumber.Id;
            $http.delete('/api/ContactNumberApi/' + Id).success(function (data) {
                $.each($scope.contactNumber, function (i) {
                    if ($scope.contactNumbers[i].Id === Id) {
                        $scope.contactNumbers.splice(i, 1);
                        return false;
                    }
                });
                $scope.loading = false;
            }).error(function (data) {
                $scope.error = "An error has occured while deleting contact number! " + data;
                $scope.loading = false;
            });
        };
    };
})();