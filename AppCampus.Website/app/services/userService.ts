app.factory("authInterceptor", function ($rootScope, $q, $window, $location, $injector, $cookies, $log, ENV) {
    var roles = [];
    var requestingRoles = false;
    function handleNotLoggedInEvent() {
        var userService = $injector.get("userService");
        userService.redirectToLogin($location.path());
    };

    function checkRoles(config) {
        var userService = $injector.get("userService");
        if (userService.isSuperUser()) {
            return true;
        } else {
            if (userService.getUserRoles().length === 0) {
                userService.redirectToLoader();
                return false;
            }
            else {
                if (config.url.indexOf("/devices") !== -1 && !hasRole("DeviceManager")) {
                    return false;
                } else {
                    return true;
                }
            }
        }
    };

    function hasRole(roleName) {
        
        var userService = $injector.get("userService");
        var hasValue = false;
        if (userService.isSuperUser()) {
            hasValue = true;
        }
        angular.forEach(userService.getUserRoles() , (role, key) => {
            if (role.roleName === roleName) {
                hasValue = true;
                return true;
            }
        });
        return hasValue;
    };

    function getUserRoles(config) {
        var userService = $injector.get("userService");
        var usersService = $injector.get("usersService");
        return userService.$resource().getRoles({ userId: JSON.parse($cookies.token).userId }).$promise;
    }

    return {
        request: function (config) {
            config.headers = config.headers || {};
            var $http = $injector.get("$http");
            if (config.url.localeCompare(ENV.apiUrl + "/api/v1/token") === 0) {
                return config;
            }
            else if (!config.headers.Authorization) {
                if ($cookies.token && JSON.parse($cookies.token).access_token) {
                    var usersService = $injector.get("usersService");
                    $http.defaults.headers.common.Authorization = "Bearer " + JSON.parse($cookies.token).access_token;
                    usersService.setCompanyId(JSON.parse($cookies.token).companyId);
                    config.headers.Authorization = 'Bearer ' + JSON.parse($cookies.token).access_token;
                    $rootScope.$broadcast("userInfoUpdate");
                } else {
                    handleNotLoggedInEvent();
                }
            }
            if ($location.path().indexOf("/login") === 0 || $location.path().indexOf("/logout") === 0) {
                roles = [];
                return config;
            }
            else if (config.url.indexOf("roles") === -1 && config.url.indexOf("html") === -1) {
                var promise = checkRoles(config);
                if (!promise) {
                    return $q.reject("requestRejector");
                } else {
                    return config;
                };
            } else {
                return config;
            }
            return config;
        },
        response: function (response) {
            return response;
        },
        responseError: function (rejection) {;
            if (rejection.status === 401) {
                var toastr = $injector.get('toastr');
                if ($location.path() !== "/login") {
                    toastr.error("You need to be logged in to access that resource", "Unauthorized");
                }
                delete $cookies.token;
                handleNotLoggedInEvent();
            }
            return $q.reject(rejection);
        }
    };
});
app.factory("userService", function ($rootScope, $resource, $log, $http, $location, $cookies, toastr, usersService, roleService, tokenService, ENV) {
    var userService = {};
    var requestUri = ENV.apiUrl + "/api/v1/companies/:companyId/users/:userId";
    var users = <any>{}; 
    var currentLocation = "";
    var username = "";
    var roles = [];

    function login(username: string, password: string, rememberMe: boolean, companyId: string) {
        if (!isUserLoggedIn() || companyId) {
            if (typeof $http.defaults.headers.common.Authorization !== "undefined") {
                var tempHolder = $http.defaults.headers.common.Authorization;
                delete $http.defaults.headers.common.Authorization;
            }

            //return false;
            tokenService.getToken($.param({
                grant_type: "password",
                username: username,
                password: password,
                cloakCompanyId: companyId
            }),
                (token) => {
                    $http.defaults.headers.common.Authorization = "bearer " + token.access_token;
                    usersService.setCompanyId(token.companyId);
                    initResource();

                    if (typeof $cookies.token !== "undefined") {
                        delete $cookies.token;
                    }

                    if (rememberMe) {
                        $cookies.token = JSON.stringify(token);
                    } else {
                        delete token.access_token;
                        $cookies.token = JSON.stringify(token);
                    }

                    $rootScope.$broadcast("userInfoUpdate");
                    toastr.success("Successfully logged in", "Success");
                    $location.path("/loader");
                },(error) => {
                    //$http.defaults.headers.common.Authorization = tempHolder;
                    $rootScope.$broadcast("loginFailed");
                    toastr.error("Could not log you in. Check username and password", "Error");
                    $location.path("/login");
                });
        } else {
            redirectBack();
        }
    };

    function isUserLoggedIn() {
        if ($http.defaults.headers.common.Authorization) {
            return true;
        }
        if ($cookies.token && JSON.parse($cookies.token).access_token) {
            return true;
        }
        return false;
    };

    function redirectToLoader() {
        if ($location.path() === "/loader") {
            return false;
        }

        currentLocation = $location.path();
        $location.path("/loader");
    }

    function redirectToLogin(location: string) {
        if ($location.path() !== "/login") {
            currentLocation = $location.path();
            
            setTimeout(function () {
                $location.path("/login");
                $rootScope.$apply();
            }, 0);
        }
    };

    function redirectBack() {
        if (typeof (currentLocation) !== "undefined" && currentLocation !== "" && currentLocation !== "/login" && currentLocation !== "/loader") {
            $location.path(currentLocation);
            //setTimeout(function () { currentLocation = ""; }, 500);
        } else {
            setTimeout(function () {
                $location.path("/");
                $rootScope.$apply();
            }, 0);
        }
    };

    function getToken() {
        if ($cookies.token && $cookies.token.access_token) {
            return $cookies.token;
        } else {
            if ($http.defaults.headers.common.Authorization) {
                return $cookies.token;
            } else {
                return null;
            }
        }
    };

    function isSuperUser() {
        if (JSON.parse(getToken()).isSuper.indexOf("True") === 0) {
            return true;
        }
    };

    function checkCompanyInfo(): boolean {
        if (isUserLoggedIn()) {
            if (getToken()) {
                usersService.setCompanyId(JSON.parse(getToken()).companyId);
                initResource();
                return true;
            } else {
                redirectToLogin($location.path());
                return false;
            }
        } else {
            return false;
        }
    };

    function getUserId(): string {
        return JSON.parse(getToken()).userId;
    };

    function initResource() {
        if (Object.getOwnPropertyNames(users).length === 0) {
            users = $resource(requestUri, { companyId:  function () { return usersService.getCompanyId() } }, {
                'updatePassword': {
                    method: 'PUT',
                    params: { userId: '@userId' },
                    url: requestUri + "/password"
                },
                'resetPassword': {
                    method: "POST",
                    params: { userId: "@userId" },
                    url: requestUri + "/password"
                },
                'update': {
                    method: "PUT",
                    params: { userId: "@userId" }
                },
                'getRoles': {
                    method: "GET",
                    params: { userId: "@userId" },
                    url: requestUri + "/roles",
                    isArray: true
                },
                "addRole": {
                    method: "POST",
                    params: { userId: "@userId" },
                    url: requestUri + "/roles"
                },
                "removeRole": {
                    method: "DELETE",
                    params: { userId: "@userId", roleId: "@roleId" },
                    url: requestUri + "/roles/:roleId"
                }
            });
        }
        return users;
    };

    function setUserRoles(userRoles) {
        roles = userRoles;
    };

    function getUserRoles() {
        return roles;
    };

    return {
        login: login,
        getCompanyId: usersService.getCompanyId,
        redirectToLoader: redirectToLoader,
        redirectToLogin: redirectToLogin,
        redirectBack: redirectBack,
        getToken: getToken,
        isUserLoggedIn: isUserLoggedIn,
        isSuperUser: isSuperUser,
        $resource: initResource,
        setUserRoles: setUserRoles,
        getUserRoles: getUserRoles,
        checkCompanyInfo: checkCompanyInfo,
        getUserId: getUserId
    };
});
app.factory("usersService", function ($resource, ENV) {
    var requestUri = ENV.apiUrl + "/api/v1/companies/:companyId/users/:userId/";
    var companyId = "";

    function getCompanyId() {
        return companyId;
    };

    function setCompanyId(company: string) {
        companyId = company;
    };

    return {
        $resource: $resource(requestUri, { companyId: getCompanyId },
            {
                assignRole: {
                    method: "POST",
                    params: { userId: "@userId" },
                    url: requestUri + "roles"
                }
            }
            ),
        getCompanyId: getCompanyId,
        setCompanyId: setCompanyId
    }
});
app.factory("roleService", function ($resource, usersService, ENV) {
    var requestUri = ENV.apiUrl + "/api/v1/roles";
    return $resource(requestUri);
});
app.factory("tokenService", function ($resource, ENV) {
    var requestUri = ENV.apiUrl + "/api/v1/token";
    return $resource(requestUri, {}, {
        getToken: {
            method: "POST",
            headers: { "Content-Type": "application/x-www-form-urlencoded" },
            //transformRequest: function (obj) {
            //    var str = [];
            //    for (var p in obj)
            //        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
            //    return str.join("&");
            //}
        }
    });
});  