app.controller('loginController', function ($rootScope, $scope, $routeParams, userService, toastr) {
    $scope.loggingIn = false;
    $rootScope.$on("loginFailed", function doSomething() {
        $scope.loggingIn = false;
    });
    if (!userService.isUserLoggedIn()) {
        userService.redirectToLogin();
    }
    $scope.login = function (form) {
        if (form.$invalid) {
            toastr.error("The specified details entered are not valid", "Error");
            return false;
        }
        $scope.loggingIn = true;
        userService.login($scope.username, $scope.password, $scope.rememberMe, $routeParams.companyId || null);
    };
});
app.controller('logoutController', function ($scope, $cookies, $http, $location) {
    delete $cookies.token;
    delete $http.defaults.headers.common.Authorization;
    delete $cookies.name;
    $location.path("#/");
});
