app.controller("devicesController", function ($scope, devicesService, signboardsService, $modal, $log, $location, toastr, dateFormatter, errorHandler) {
    $scope.formatDate = dateFormatter.formatDate;
    $scope.showModal = false;
    $scope.toggleModal = function () {
        $scope.showModal = !$scope.showModal;
    };
    $(".content").slimscroll({
        height: window.innerHeight - 135,
        alwaysVisible: true,
        size: "3px"
    });
    getDevices();
    function getDevices() {
        var signboards = devicesService.query()
            .$promise.then(function (data) {
            $scope.devices = data;
        }, function (error) {
            $scope.devices = [];
            errorHandler.handleError(error, null, "Error listing devices", "Error");
        });
    }
    ;
    $scope.open = function (size, device) {
        $scope.selectedDevice = device;
        var modalInstance = $modal.open({
            templateUrl: 'myModalContent.html',
            controller: 'deviceApproveModal',
            size: size,
            resolve: {}
        });
        modalInstance.result.then(function (newName) {
            $scope.newSignboardName = newName;
            var signboard = new signboardsService({ "Name": $scope.newSignboardName, "MacAddress": $scope.selectedDevice.macAddress });
            signboard.$save({}, function success(data) {
                toastr.success("New signboard created", "Success");
                $location.path("/signboards");
            }, function (error) {
                errorHandler.handleError(error, {}, "Signboard not created");
            });
        }, function () {
        });
    };
    $scope.decline = function (device) {
        if (device.comment === null) {
            device.comment = "a";
        }
        device.$declined({ action: "declined" }, function success(data) {
            toastr.success("Device declined");
        }, function error(data) {
            errorHandler.handleError(error, {}, "An error occured when trying to decline the device");
        });
    };
    $scope.block = function (device) {
        if (device.comment === null) {
            device.comment = "a";
        }
        device.$blocked({ action: "blocked" }, function success(data) {
            toastr.success("Device blocked");
        }, function error(error) {
            errorHandler.handleError(error, {}, "An error occurred when trying to block the device");
        });
    };
    $scope.orderDevices = function (device) {
        var score = 0;
        if (device.state === "Pending")
            score += 10;
        if (device.state === "Declined")
            score += 20;
        if (device.state === "Blocked")
            score += 30;
        score += Date.parse(device.lastRequestDate) * (1 / 10000000000000);
        return score;
    };
});
app.controller('deviceApproveModal', function ($scope, $timeout, $modalInstance) {
    $scope.signboardName = "";
    $scope.warningText = "";
    $scope.ok = function () {
        $scope.signboardName = $scope.signboardName.trim();
        if ($scope.signboardName !== "") {
            $modalInstance.close($scope.signboardName);
        }
        else {
            $scope.warningText = "You need to specify a name";
            $timeout(function () { $scope.warningText = ""; }, 2000);
        }
    };
    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
});
