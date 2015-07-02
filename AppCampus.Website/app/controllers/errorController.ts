app.controller("errorController", ["$scope", "$location", function ($scope, $location) {
    setTimeout(function () {
        $location.path("/logout");
    }, 5000);
}]); 