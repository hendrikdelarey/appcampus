app.controller("usersController",($scope, userService, toastr, errorHandler) => {
    $(".content").slimscroll({
        height: window.innerHeight - 135,
        alwaysVisible: true,
        size: "3px"
    });

    userService.$resource().query({},
        function success(users) {
            $scope.users = users;
        }, errorHandler.handleError);
}); 
app.controller("userController",($scope, $routeParams, $location, userService, toastr, errorHandler, roleService) => {
    $scope.createUser = false;
    if ($routeParams.id === "new") {
        $scope.createUser = true;
        $scope.user = {};
    }

    init();

    function init() {
        //$scope.savingUser = true;
        $(".content").slimscroll({
            height: window.innerHeight - 135,
            alwaysVisible: true,
            size: "3px"
        });

        roleService.query().
            $promise.then(
            function (roles) {
                $scope.roles = roles;
            }, errorHandler.handleError);

        userService.$resource().get({ userId: $routeParams.id },
            function (user) {
                $scope.user = user;
            }, errorHandler.handleError);

        getUsersRoles();
    }

    function getUsersRoles() {
        userService.$resource().getRoles({ userId: $routeParams.id },
            function (userRoles) {
                $scope.userRoles = userRoles;
            }, errorHandler.handleError);
    }

    $scope.updateUserDetails = function (form) {
        if ($routeParams.id === "new") {
            if (form.$valid) {
                $scope.savingUser = true;
                userService.$resource().save({
                    userName: $scope.user.userName,
                    firstName: $scope.user.firstName,
                    lastName: $scope.user.lastName
                },
                    function success(user) {
                        $scope.savingUser = false;
                        toastr.success("User successfully created");
                        $location.path("/users/" + user.userId);
                    }, function error(error) {
                        $scope.savingUser = false;
                        errorHandler.handleError(error);
                    });
            }
        } else {
            $scope.savingUser = true;
            $scope.user.$update({ userId: $routeParams.id },
                function success(user) {
                    $scope.savingUser = false;
                    setTimeout(function () {
                        $scope.$apply();
                        toastr.success("User information successfully updated");
                    }, 0);
                },
                function error(error) {
                    $scope.savingUser = false;
                    errorHandler.handleError(error);
                });
        }
    };

    $scope.resetUserPassword = function (form) {
        var user = angular.copy($scope.user);
        $scope.resettingPassword = true;
        user.$resetPassword({},
            function success(user) {
                $scope.resettingPassword = false;
                toastr.success("User password successfully updated");
            },
            function error(error) {
                $scope.resettingPassword = false;
                errorHandler.handleError(error);
            });
    };

    $scope.assignRole = function () {
        if (typeof $scope.selectedRole === "undefined" || !$scope.selectedRole) {
            return false;
        }
        $scope.user.roleId = $scope.selectedRole;
        var userToRole = angular.copy($scope.user);
        $scope.savingRoles = true;
        userToRole.$addRole({},
            function success() {
                $scope.savingRoles = false;
                delete $scope.selectedRole;
                toastr.success("Role successfully added");
                getUsersRoles();
            },
            function error(error) {
                $scope.savingRoles = false;
                if (error.data.modelState[""][0] === "User already in role.") {
                    errorHandler.handleError(error, {}, error.data.modelState[""][0]);
                }
            });
    };

    $scope.removeRole = function (role) {
        $scope.deletingRole = true;
        role.deleting = true;
        $scope.user.roleId = role.roleId;
        $scope.user.$removeRole({},
            function success() {
                toastr.success("Role removed");
                getUsersRoles();
                $scope.deletingRole = false;
            },
            function error(error) {
                errorHandler.handleError(error);
                $scope.deletingRole = false;
                role.deleting = false;
            });
    };

    $scope.filterRoles = function (value) {
        var searchVal = false;
        angular.forEach($scope.userRoles, (role, key) => {
            if (value.roleName === role.roleName) {
                searchVal = true;
            }
        });
        if (searchVal === false) {
            return value;
        } else {
            return null;
        }
    };

});