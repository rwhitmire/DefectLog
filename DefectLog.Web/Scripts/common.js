var common = {
    baseUrl: '/',
    dataStore: {}
};

$.fn.hide = function() {
    this.addClass('hidden');
};

$.fn.show = function() {
    this.removeClass('hidden');
};

angular.module('defectLog', []);