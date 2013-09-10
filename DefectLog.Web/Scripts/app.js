(function(app) {

    var DefectCtrl = function ($scope, $http, $q) {
        'use strict';

        var baseUrl = common.baseUrl + 'api/defect';
        var validator = $('#defect-form').validate();

        $scope.users = common.dataStore.users;
        $scope.statuses = common.dataStore.statuses;
        $scope.currentUserId = common.dataStore.currentUserId;
        $scope.versions = common.dataStore.versions;
        $scope.categories = common.dataStore.categories;
        $scope.priorityLevels = common.dataStore.priorityLevels;
        $scope.defects = [];
        $scope.defect = null;
        $scope.versionId = common.dataStore.defaultVersion;
        $scope.priorityLevelId = common.dataStore.defaultPriorityLevel;

        $scope.filterApplied = false;

        $scope.defectClicked = function (defect) {
            console.log('loading defect:', defect);
            $scope.form = angular.copy(defect);
            $scope.defect = defect;
            $scope.scrollToForm();
        };

        $scope.formValid = function() {
            return $('#defect-form').valid();
        };

        $scope.saveDefect = function () {
            if (!$scope.formValid()) return $q.when();

            var id = $scope.form.id;
            var xhr;
            var form = $scope.form;

            var data = {
                id: form.id,
                dateLogged: form.dateLogged,
                developerId: form.developerId,
                statusId: form.statusId,
                summary: form.summary,
                testerId: form.testerId,
                appVersionId: form.appVersionId,
                comment: form.comment,
                userId: $scope.currentUserId,
                build: form.build,
                screen: form.screen,
                categoryId: form.categoryId,
                priorityLevelId: form.priorityLevelId
            };

            console.log('saving: ', data);

            if (id) xhr = $http.put(baseUrl + '/' + id, data);
            else xhr = $http.post(baseUrl, data);

            return xhr.then(function () {
                $scope.form = null;
                $scope.scrollToTop();
                return $scope.loadDefects();
            });
        };

        $scope.newDefect = function () {
            console.log($scope.statuses[0].id);

            $scope.form = {
                statusId: $scope.statuses[0].id,
                testerId: $scope.currentUserId,
                dateLogged: new Date(),
                appVersionId: $scope.versionId,
                priorityLevelId: $scope.priorityLevelId
            };

            $scope.scrollToForm();
        };

        $scope.closeForm = function () {
            $scope.form = null;
            $scope.scrollToTop();
        };

        $scope.$watch('form', function () {
            $scope.resetValidator();
        });

        $scope.resetValidator = function() {
            validator.resetForm();
        };

        $scope.loadDefects = function () {
            if (!$scope.versionId) return $q.when();

            var url = baseUrl + '?' + $.param({ versionId: $scope.versionId });

            return $http.get(url).success(function (defects) {
                console.log('defects loaded:', defects);
                $scope.allDefects = defects;
                $scope.defects = defects;
            });
        };

        $scope.developerMeClicked = function () {
            $scope.form.developerId = $scope.currentUserId;
        };

        $scope.versionChanged = function () {
            $scope.loadDefects();
            $scope.form = null;
        };

        $scope.scrollToForm = function () {
            $('html, body').animate({
                scrollTop: $('#scroll-to-form').offset().top - 70
            }, 100);
        };

        $scope.scrollToTop = function () {
            $('html, body').animate({
                scrollTop: 0
            }, 100);
        };

        $scope.statusFilter = {
            statusId: ''
        };

        $scope.filterStatus = function (status) {
            $scope.statusFilter.statusId = status.id;
        };

        $scope.showStatus = function (status) {
            if (!$scope.statusFilter.statusId) return true;

            $scope.filterApplied = true;
            return $scope.statusFilter.statusId == status.id;
        };

        $scope.clearFilter = function () {
            $scope.statusFilter.statusId = '';
            $scope.filterApplied = false;
        };

        $scope.truncate = function (str, len) {
            if (!str) return '';
            if (str.length <= len) return str;
            return str.substr(0, len) + '...';
        };

        $scope.getDefectCount = function () {
            return $scope.defects.length;
        };

        $scope.getStatusCount = function (id) {
            var items = _($scope.defects).filter(function (item) {
                return item.statusId == id;
            });

            return items.length;
        };

        $scope.loadDefects();
    };

    app.controller('DefectCtrl', ['$scope', '$http', '$q', DefectCtrl]);

}(angular.module('defectLog', [])));
