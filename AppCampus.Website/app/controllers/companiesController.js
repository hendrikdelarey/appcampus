app.controller("companiesController", function ($scope, $modal, $location, companyService, errorHandler) {
    var init = (function () {
        loadCompanies();
        $(".content").slimscroll({
            height: window.innerHeight - 135,
            alwaysVisible: true,
            size: "3px"
        });
    })();
    function loadCompanies() {
        $scope.companiesLoading = true;
        companyService.query().$promise.then(function success(companies) {
            $scope.companies = companies;
            $scope.companiesLoading = false;
        }, function error(error) {
            errorHandler.handleError(error);
            $scope.companiesLoading = false;
        });
    }
    ;
    $scope.createCompany = function () {
        var modalInstance = $modal.open({
            templateUrl: "createCompanyModal.html",
            controller: "createCompanyModal",
            size: "",
            resolve: {}
        });
        modalInstance.result.then(function (companyName) {
            var company = new companyService({ name: companyName });
            $scope.companiesSaving = true;
            company.$save({}, function success() {
                loadCompanies();
                $scope.companiesSaving = false;
            }, function error(error) {
                errorHandler.handleError(error);
                $scope.companiesSaving = false;
            });
        });
    };
    $scope.deleteCompany = function (selectedCompany) {
        var modalInstance = $modal.open({
            templateUrl: "deleteModal.html",
            controller: "deleteCompanyModal",
            size: "",
            resolve: {}
        });
        modalInstance.result.then(function () {
            selectedCompany.$delete({}, function success() {
            }, errorHandler.handleError);
        });
    };
    $scope.cloak = function (company) {
        $location.path("/login/" + company.companyId);
    };
});
app.controller("createCompanyModal", function ($scope, $modalInstance) {
    $scope.ok = function (form) {
        if (form.$valid) {
            $modalInstance.close($scope.company.name);
        }
    };
    $scope.cancel = function () {
        $modalInstance.dismiss("cancel");
    };
});
app.controller("deleteCompanyModal", function ($scope, $modalInstance) {
    $scope.ok = function (form) {
        $modalInstance.close();
    };
    $scope.cancel = function () {
        $modalInstance.dismiss("cancel");
    };
});
