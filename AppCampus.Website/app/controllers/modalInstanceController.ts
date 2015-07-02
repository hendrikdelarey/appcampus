app.controller('ModalInstanceController', function ($scope, $timeout, $modalInstance, items, name) {
    $scope.signboardName = "";
    $scope.warningText = "";

    $scope.ok = function () {
        $scope.signboardName = $scope.signboardName.trim()
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