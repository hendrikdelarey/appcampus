/// <reference path="../content/js/typings/angularjs/angular.d.ts" />
var app = angular.module("AppCampus", ["ngRoute", "ngCookies", "ngResource", "ui.bootstrap", "ngAnimate", "toastr", "naif.base64", "ui.multiselect", "color-picker"]);

app.config(function ($routeProvider, $httpProvider) {
    $httpProvider.interceptors.push('authInterceptor');
    $routeProvider.when("/signboards", {
        controller: "signboardsController",
        templateUrl: "/app/views/signboards.html"
    });
    $routeProvider.when("/devices", {
        controller: "devicesController",
        templateUrl: "/app/views/devices.html"
    });
    $routeProvider.when("/signboards/:id", {
        controller: "signboardController",
        templateUrl: "/app/views/signboard.html"
    });
    $routeProvider.when("/announcements/", {
        controller: "announcementsController",
        templateUrl: "/app/views/announcements.html"
    });
    $routeProvider.when("/announcements/:id", {
        controller: "announcementController",
        templateUrl: "/app/views/announcement.html"
    });
    $routeProvider.when("/slideshows", {
        controller: "slideshowsController",
        templateUrl: "/app/views/slideshows.html"
    });
    $routeProvider.when("/slideshows/:id", {
        controller: "slideshowController",
        templateUrl: "/app/views/slideshow.html"
    });
    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "/app/views/login.html"
    });
    $routeProvider.when("/login/:companyId", {
        controller: "loginController",
        templateUrl: "/app/views/login.html"
    });
    $routeProvider.when("/logout", {
        controller: "logoutController",
        templateUrl: "/app/views/home.html"
    });
    $routeProvider.when("/profile", {
        controller: "profileController",
        templateUrl: "/app/views/profile.html"
    });
    $routeProvider.when("/users", {
        controller: "usersController",
        templateUrl: "/app/views/users.html"
    });
    $routeProvider.when("/users/:id", {
        controller: "userController",
        templateUrl: "/app/views/user.html"
    });
    $routeProvider.when("/companies", {
        controller: "companiesController",
        templateUrl: "/app/views/companies.html"
    });
    $routeProvider.when("/software", {
        controller: "softwaresController",
        templateUrl: "/app/views/software.html"
    });
    $routeProvider.when("/software/:id/file", {
        controller: "softwareController",
    });
    $routeProvider.when("/", {
        controller: "homeController",
        templateUrl: "/app/views/home.html"
    });
    $routeProvider.when("/loader", {
        controller: "loaderController",
        templateUrl: "/app/views/loader.html"
    });
    $routeProvider.when("/error", {
        controller: "errorController",
        templateUrl: "/app/views/error.html"
    });

    $routeProvider.otherwise({ redirectTo: "/" });
});

window.onload = function () {
    angular.bootstrap(document, ['AppCampus']);
    setTimeout(function () {
        $(".cover-window").hide();
    }, 1000);
    
};