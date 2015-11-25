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
})();