app.controller("loaderController", function ($rootScope, $cookies, $location, userService, roleService, toastr) {
    //show the loader screen
    $(".cover-window").show();

    $rootScope.$broadcast("loadingRoles");
    userService.$resource().getRoles({ userId: JSON.parse($cookies.token).userId }).$promise.then(
        function success(roles) {
            userService.setUserRoles(roles);
            userService.redirectBack();
            $rootScope.$broadcast("userInfoUpdate");
            $rootScope.$broadcast("rolesUpdated");
        },
        function error(error) {
            if (userService.isSuperUser()) {
                userService.redirectBack();
                $rootScope.$broadcast("userInfoUpdate");
                $rootScope.$broadcast("rolesUpdated");
            } else {
                toastr.error("Cannot load user roles", "Error");
                $location.path("/error");
            }
        });
});