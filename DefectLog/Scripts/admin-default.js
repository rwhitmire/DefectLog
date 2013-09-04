(function(app) {
    'use strict';

    var AdminDefaultCtrl = function ($scope, $http) {
        $scope.users = common.dataStore.newUsers;

        $scope.approveUser = function (userId, isApproved) {
            var url = common.baseUrl + 'admin/users/approveuser';
            $http.post(url, { id: userId, isApproved: isApproved });

            $scope.users = _($scope.users).filter(function (item) {
                return item.id != userId;
            });
        };
    };

    app.controller('AdminDefaultCtrl', ['$scope', '$http', AdminDefaultCtrl]);

}(angular.module('defectLog', [])));