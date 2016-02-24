'use strict';
var app = angular.module('app', [ 'restangular', 'ngTable' ])

.config(["RestangularProvider", function (RestangularProvider) {
    // Specify the base URL for all Restangular calls by prepending '/api' to all url's 
    RestangularProvider.setBaseUrl('/api');

    RestangularProvider.addResponseInterceptor(function (data, operation, what, url, response, deferred) {
        var extractedData;
        // .. to look for getList operations
        if (operation === "getList") {
            // .. and handle the data and meta data
            extractedData = data.items;
            extractedData.paging =
            {
                pageCount: data.pageCount,
                pageNo: data.pageNo,
                pageSize: data.pageSize,
                totalRecordCount: data.totalRecordCount
            };
        } else {
            extractedData = data;
        }
        return extractedData;
    });

}]);
