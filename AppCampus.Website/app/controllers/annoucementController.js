app.controller("announcementsController", function ($scope, $log, $modal, announcementService, dateFormatter, errorHandler) {
    init();
    function init() {
        loadAnnouncements();
        $(".content").slimscroll({
            height: window.innerHeight - 135,
            alwaysVisible: true,
            size: "3px"
        });
    }
    ;
    function loadAnnouncements() {
        announcementService.query().
            $promise.then(function success(announcements) {
            $scope.announcements = announcements;
        }, function error(error) {
            errorHandler.handleError(error);
        });
    }
    ;
    $scope.formatDate = dateFormatter.formatDate;
    $scope.enableAnnouncement = function (announcement) {
        announcement.isActive = true;
        announcement.$update({ announcementId: announcement.announcementId }, function success() { }, function error(error) {
            errorHandler.handleError(error);
        });
    };
    $scope.disableAnnouncement = function (announcement) {
        announcement.isActive = false;
        announcement.$update({ announcementId: announcement.announcementId }, function success() { }, function error(error) {
            errorHandler.handleError(error);
        });
    };
    $scope.deleteAnnouncement = function (announcement) {
        var modalInstance = $modal.open({
            templateUrl: "deleteModal.html",
            controller: "announcementDeleteModal",
            size: "",
            resolve: {}
        });
        modalInstance.result.then(function (confirmation) {
            $scope.deletingAnnouncement = true;
            announcement.deletingAnnouncement = true;
            announcement.$delete({ announcementId: announcement.announcementId }, function success(data) {
                loadAnnouncements();
                $scope.deletingAnnouncement = false;
            }, function error(error) {
                errorHandler.handleError(error);
                $scope.deletingAnnouncement = false;
                announcement.deletingAnnouncement = false;
            });
        });
    };
});
app.controller("announcementController", function ($scope, $routeParams, $location, $log, signboardsService, assignedSignboardsService, announcementService, dateFormatter, toastr, errorHandler) {
    $scope.types = [
        { name: "General" },
        { name: "Critical" },
    ];
    init();
    function init() {
        if ($routeParams.id === "new") {
            $scope.announcement = new announcementService();
            $scope.hideAssign = true;
        }
        else {
            loadAnnouncement();
        }
        getSignboards();
        $(".content").slimscroll({
            height: window.innerHeight - 135,
            alwaysVisible: true,
            size: "3px"
        });
    }
    ;
    function getSignboards() {
        signboardsService.query().
            $promise.then(function (signboards) {
            $scope.signboards = signboards;
        }, errorHandler.handleError);
    }
    ;
    function loadAnnouncement() {
        announcementService.get({ "announcementId": $routeParams.id })
            .$promise.then(function (data) {
            $("#endDate").val(new Date(data.endDate).toLocaleString());
            $("#startDate").val(new Date(data.startDate).toLocaleString());
            $scope.announcement = data;
            $scope.allowAssigning = true;
            loadAssignedSignboards();
        });
    }
    ;
    function loadAssignedSignboards() {
        $scope.loadingAssigned = true;
        assignedSignboardsService.query({ announcementId: $scope.announcement.announcementId }).
            $promise.then(function (assignedSignboards) {
            $scope.assignedSignboardIds = assignedSignboards;
            var assignedSignboardsIds = assignedSignboards;
            var numberOfSignboards = assignedSignboards.length;
            var currentNumber = 0;
            if (numberOfSignboards === 0) {
                $scope.loadingAssigned = false;
            }
            angular.forEach(assignedSignboardsIds, function (value, key) {
                signboardsService.get({ "signboardId": value.signboardId }).$promise.then(function (signboard) {
                    if (typeof ($scope.assignedSignboards) === "undefined") {
                        $scope.assignedSignboards = [];
                    }
                    ;
                    pushIfNotExists(signboard);
                    currentNumber++;
                    if (currentNumber === numberOfSignboards) {
                        $scope.loadingAssigned = false;
                    }
                });
            });
        });
    }
    ;
    function pushIfNotExists(signboard) {
        var canPush = true;
        angular.forEach($scope.assignedSignboards, function (value, key) {
            if (value.signboardId == signboard.signboardId) {
                canPush = false;
            }
        });
        if (canPush) {
            $scope.assignedSignboards.push(signboard);
        }
    }
    ;
    function getIndex(signboardId) {
        var returnValue;
        angular.forEach($scope.assignedSignboardIds, function (value, key) {
            if (value.signboardId === signboardId) {
                returnValue = key;
            }
        });
        return returnValue;
    }
    function getIndexSignboard(signboardId) {
        var returnValue;
        angular.forEach($scope.assignedSignboards, function (value, key) {
            if (value.signboardId === signboardId) {
                returnValue = key;
            }
        });
        return returnValue;
    }
    function getIndexOfSignboard(signboardName) {
        var returnVal = {};
        angular.forEach($scope.signboards, function (signboard, key) {
            if (signboard.name == signboardName) {
                returnVal = signboard;
            }
        });
        return returnVal;
    }
    $scope.$on('emit:dateTimePicker', function (e, value) {
        if (value.element.id == "start-date") {
            $scope.announcement.startDate = new Date(value.dateTime);
            $("#startDate").val($scope.announcement.startDate.toLocaleString());
        }
        if (value.element.id == "end-date") {
            $scope.announcement.endDate = new Date(value.dateTime);
            $("#endDate").val($scope.announcement.endDate.toLocaleString());
        }
    });
    $scope.saveAnnouncement = function (announcementForm) {
        $scope.$broadcast('show-errors-event');
        if (!announcementForm.$valid) {
            return false;
        }
        $scope.creatingAnnouncement = true;
        var saveAnnouncement = angular.copy($scope.announcement);
        saveAnnouncement.startDate = new Date(saveAnnouncement.startDate).toISOString();
        saveAnnouncement.endDate = new Date(saveAnnouncement.endDate).toISOString();
        if ($routeParams.id === "new") {
            saveAnnouncement.$save({}, function success() {
                $scope.announcement = angular.copy(saveAnnouncement);
                $location.path("/announcements/" + $scope.announcement.announcementId);
                toastr.success("Announcement created", "Success");
                $scope.creatingAnnouncement = false;
            }, function error(error) {
                errorHandler.handleError(error);
                $scope.creatingAnnouncement = false;
            });
        }
        else {
            saveAnnouncement.$update({ 'announcementId': $scope.announcement.announcementId }, function success() { $scope.announcement = saveAnnouncement; toastr.success("Announcement updated", "Success"); $scope.creatingAnnouncement = false; }, function error(error) { errorHandler.handleError(error); $scope.creatingAnnouncement = false; });
        }
    };
    $scope.assignThisAnnouncementToSelectedSignboard = function () {
        if (typeof ($scope.selectedSignboard) !== "undefined") {
            if ($routeParams.id !== "new") {
                var newBoards = angular.copy($scope.assignedSignboards) || [];
                angular.forEach(newBoards, function (value, index) {
                    delete value.name;
                    delete value.macAddress;
                });
                angular.forEach($scope.selectedSignboard, function (value, index) {
                    newBoards.push({ "signboardId": getIndexOfSignboard(value).signboardId });
                });
                var assignedSignboard = new assignedSignboardsService($.param(newBoards));
                $scope.assigningSignboard = true;
                assignedSignboardsService.update({ "announcementId": $scope.announcement.announcementId }, newBoards, function success() {
                    $scope.assigningSignboard = false;
                    loadAssignedSignboards();
                    delete $scope.selectedSignboard;
                }, function error(error) {
                    $scope.assigningSignboard = false;
                });
            }
        }
    };
    $scope.unassignThisAnnouncementFromSelectedSignboard = function (signboard, index) {
        var elementToRemove = $scope.assignedSignboardIds.splice(getIndex(signboard.signboardId), 1);
        signboard.removing = true;
        $scope.removingSignboard = true;
        assignedSignboardsService.update({ "announcementId": $scope.announcement.announcementId }, $scope.assignedSignboardIds, function success(assignedSignboards) {
            $scope.assignedSignboards.splice(index, 1);
            toastr.success("Signboard removed", "Success");
            $scope.removingSignboard = false;
        }, function (errorMessage) {
            $scope.assignedSignboardIds.push(elementToRemove);
            toastr.error("Error removing element", "Error");
            signboard.removing = false;
            $scope.removingSignboard = false;
        });
    };
    $scope.assignedSignboardsFilter = function (value) {
        var searchVal = false;
        angular.forEach($scope.assignedSignboards, function (signboard, key) {
            if (value.name === signboard.name) {
                searchVal = true;
            }
        });
        if (searchVal === false) {
            return value;
        }
    };
});
app.controller("announcementDeleteModal", function ($scope, $modalInstance) {
    $scope.ok = function () {
        $modalInstance.close(true);
    };
    $scope.cancel = function () {
        $modalInstance.dismiss("cancel");
    };
});
