app.controller("softwaresController", function ($scope, $modal, $http, ENV, toastr, softwareService, errorHandler) {
    (function init() {
        loadSoftwareList();
        $(".content").slimscroll({
            height: window.innerHeight - 135,
            alwaysVisible: true,
            size: "3px"
        });
    })();
    function loadSoftwareList() {
        softwareService.query({}).$promise.then(function success(softwares) {
            $scope.softwares = softwares;
        }, errorHandler);
    }
    ;
    $scope.createSoftware = function (announcement) {
        var modalInstance = $modal.open({
            templateUrl: "createModal.html",
            controller: "createSoftwareModal",
            size: "",
            resolve: {}
        });
        modalInstance.result.then(function (softwarePkg) {
            var software = new softwareService({ version: softwarePkg.version });
            $scope.uploadingSoftware = true;
            software.$save({}, function success() {
                var fd = new FormData();
                fd.append('file', softwarePkg.file);
                $http.post(ENV.apiUrl + "/api/v1/software/" + software.softwareId + "/file", fd, {
                    transformRequest: angular.identity,
                    headers: { "Content-Type": undefined }
                })
                    .success(function () {
                    $scope.uploadingSoftware = false;
                    loadSoftwareList();
                })
                    .error(function (error) {
                    errorHandler.handleError(error);
                    $scope.uploadingSoftware = false;
                });
            }, function error(error) {
                errorHandler.handleError(error);
                $scope.uploadingSoftware = false;
            });
        });
    };
    $scope.downloadPackage = function (softwarePackage) {
        var link = document.createElement("a");
        link.style.width = "1px";
        link.style.height = "1px";
        var elem = document.getElementById("newResource");
        elem.appendChild(link);
        window.location.assign(ENV.apiUrl + "/api/v1/software/" + softwarePackage.softwareId + "/file");
    };
});
app.controller("softwareController", function ($scope, $routeParams) {
});
app.controller("createSoftwareModal", function ($scope, $modalInstance) {
    $scope.ok = function ($form) {
        if ($form.$invalid) {
            return false;
        }
        $modalInstance.close($scope.software);
    };
    $scope.cancel = function () {
        $modalInstance.dismiss("cancel");
    };
});
