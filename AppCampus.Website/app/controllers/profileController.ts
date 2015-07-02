app.controller("profileController",($scope, $rootScope, userService, errorHandler, toastr) => {
    $(".content").slimscroll({
        height: window.innerHeight - 135,
        alwaysVisible: true,
        size: "3px"
    });

    userService.$resource().get({ userId: userService.getUserId() },
        function success(user) {
            $scope.user = user;
        }, errorHandler.handleError);

    $scope.updateUserDetails = function (form) {
        $scope.updatingUserDetails = true;
        $scope.user.$update({},
            function success() {
                $scope.updatingUserDetails = false;
                toastr.success("User information updated");
                $rootScope.$broadcast("userInfoUpdate");
            }, function error(error) {
                $scope.updatingPassword = false;
                errorHandler.handleError(error);
            });
    };

    $scope.updatePassword = function (form) {
        $scope.updatingPassword = true;
        if (form.$invalid) {
            return false;
        }
        $scope.user.$updatePassword({},
            function success(user) {
                toastr.success("Password changed");
                $scope.updatingPassword = false;
            }, function error(error) {
                $scope.updatingPassword = false;
                errorHandler.handleError(error);
            });
    };
});