var countModule = angular.module("countModule", []);

countModule.controller("countCtrl", function ($scope, $http, $element) {
    $scope.countModel = {
        CountLessThan10Mb:  0,
        CountBetween10MbAnd50Mb: 0,
        CountMoreThan100Mb: 0,
        CurrentPath: " ",
        DirectoryName: []
    }

        $http({
            method: 'GET',
            url: "/api/values",
            headers: { 'Accept': 'application/json' }
        })
            .then(function (responseData) {
                $scope.countModel = responseData.data;
            }, function (x) { // Request error
            })


    $scope.getCount = function(e) {
        $http({
            method: 'GET',
            url: "/api/values",
            params: {
                path: e.currentTarget.text,
                currentPath: $scope.countModel.CurrentPath
            },
            headers: { 'Accept': 'application/json' }
            })
            .then(function (responseData) {
                $scope.countModel = responseData.data;
            }, function (x) { // Request error
            })
    }
});