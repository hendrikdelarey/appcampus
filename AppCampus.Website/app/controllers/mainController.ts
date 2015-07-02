app.controller("mainController", function ($scope, $rootScope, $http, $cookies, $injector, userService, usersService, errorHandler) {
    var devicesService = <any>{};
    $scope.newDevices = false;
    $scope.superAdmin = false;
    
    //userLogin();

    function init() {
        userLogin();
        checkDevices();
        setInterval(function () { checkDevices(); }, 30000);
    };

    function checkDevices() {
        if (!userService.checkCompanyInfo()) {
            return false;
        } else {
            devicesService = $injector.get("devicesService");
            devicesService.query({}).$promise.then(
                function success(devices) {
                $scope.newDevices = false;
                angular.forEach(devices,(value, key) => {
                    if (value.state === "Pending") {
                        $scope.newDevices = true;
                    }
                });
            },
                (error) => {
                    errorHandler.handleError(error);
                });
        }
    };

    function userLogin() {
        applyRoutes();
    };

    function applyRoutes() {
    };

    $scope.$on("userInfoUpdate", function (event, args) {
        $scope.superAdmin = JSON.parse($cookies.token).isSuper;

        userService.$resource().get({
            userId: JSON.parse($cookies.token).userId
        },
            function success(user) {
                init();
                $scope.name = user.firstName + " " + user.lastName;
            },
            (error) => {
                if (!userService.isSuperUser()) {
                    errorHandler.handleError(error);
                }
            });
        var companyService = $injector.get("companyService");
        companyService.get({},
            (company) => {
                $scope.companyName = company.name;
                $scope.companyId = company.companyId;
            },
            (error) => {
                errorHandler.handleError(error);
            });
    });
    
    $scope.$on("rolesUpdated", function (event, args) {
        $(".cover-window").hide();
    });    

    $scope.$on("loadingRoles", function (event, args) {
        $scope.loaderNumber = "(1/1)";
        $scope.loaderText = "Loading Roles";
    });
}); 