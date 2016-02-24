'use strict';
app.controller('CustomersController', ["$scope", "Restangular", "ngTableParams", function ($scope, Restangular, ngTableParams) {

    $scope.title = 'Customers';
    $scope.customers = [];
    $scope.search = '';

    $scope.tableParams = new ngTableParams(
        // Defaults
        {
            page: 1,
            count: 10,
            sorting: {lastName: 'asc'}
        },
        {
            getData: function ($defer, params) {
                Restangular.all('customers').getList(
                    {
                        pageNo: params.page(),
                        pageSize: params.count(),
                        sort: params.orderBy(),
                        search: $scope.search
                    }
                ).then(
                    function (customers) {
                        // Tell ngTable how many records we have so it can update paging controls.
                        params.total(customers.paging.totalRecordCount);

                        // Return the customers to ngTable
                        $defer.resolve(customers);
                    },

                    function (error) {

                    }
                );
            }
        }
    );

    $scope.$watch('search', function (oldValue, newValue) {
        // Relaod data when user enters a search criteria
        $scope.tableParams.reload();
    });

}]);
