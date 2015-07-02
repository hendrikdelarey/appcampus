interface JQuery {
    knob: Function;
    sparkline: Function;
    datetimepicker: Function;
    slimscroll: Function;
}

app.controller("signboardsController", function ($scope, signboardsService, $filter, $http, userService, toastr, dateFormatter,diagnosticService, scheduledSlideshowService, slideshowService, errorHandler) {
    getSignboards();
    $scope.dateFormatter = dateFormatter;

    $(".content").slimscroll({
        height: window.innerHeight - 135,
        alwaysVisible: true,
        size: "3px"
    });

    $scope.showModal = false;
    $scope.toggleModal = function () {
        $scope.showModal = !$scope.showModal;
    };

    function getSignboards(): void {
        var signboards = signboardsService.query()
            .$promise.then(function (data) {
                $scope.signboards = data;

                angular.forEach($scope.signboards,(value, index) => {
                    diagnosticService.latest({ signboardId: value.signboardId }).$promise.then(
                        function (signboardDiagnostics) {
                            value.diagnosticDate = signboardDiagnostics.diagnosticDate || 0;
                            value.softwareVersion = signboardDiagnostics.softwareVersion || 0;
                        },
                        function error(error) {
                            value.diagnosticDate = "failed";
                            value.softwareVersion = "failed";
                            errorHandler.handleError(error);
                        });
                });
                angular.forEach($scope.signboards, (signboard, index) => {
                    scheduledSlideshowService.query({ "signboardId": signboard.signboardId }).$promise.then(
                        function success(scheduledSlideshows) {
                            //loop through each slideshow and see which one is the latest
                            var latestDate = new Date(1999, 12, 31);
                            angular.forEach(scheduledSlideshows,(scheduledSlideshow, key) => {
                                if (new Date(scheduledSlideshow.startDate) > latestDate) {
                                    if (new Date(scheduledSlideshow.startDate) <= new Date()) {
                                        latestDate = new Date(scheduledSlideshow.startDate);
                                        signboard.activeSlideshowId = scheduledSlideshow.slideshowId;
                                    }
                                }
                            });
                            if (typeof (signboard.activeSlideshowId) !== "undefined") {
                                getActiveSlideshowName(signboard);
                            } else {
                                signboard.activeSlideshowId = 0;
                            }
                        },
                        function error(error) {
                            signboard.activeSlideshowId = "failed";
                            errorHandler.handleError(error);
                        });
                });
            }, function error(error) {
                errorHandler.handleError(error, {}, "Unable to get Signboards");
            });
    };

    function getActiveSlideshowName(signboard) {
        slideshowService.get({ "slideshowId": signboard.activeSlideshowId },
            function success(slide) {
                signboard.activeSlideshowName = slide.name;
            },
            function error(error) {
                signboard.activeSlideshowName = "failed";
                errorHandler.handleError(error, {}, "Could not get active slide show for " + signboard.activeSlideshowId);
            });
    };

    $scope.allowThisSlideName = function (linkName) {
        if (linkName.indexOf("fail") !== 0) {
            return linkName;
        }
    }
});

app.controller("signboardController", function ($scope, $routeParams, $log, $filter, $modal, signboardsService, slideshowService, scheduledSlideshowService,
    announcementService, assignedSignboardsService, diagnosticService, dateFormatter, errorHandler, toastr,
    softwareService) {
    init();

    function init() {
        $(".cpu").knob({ width: 5});
        $(".disk-space").knob();
        $(".memory").knob();

        $scope.signboardId = $routeParams.id;
        $scope.dateFormatter = dateFormatter;
        $scope.monitor = true;
        $scope.management = false;

        getScheduledSlideshows();
        getSlideshows();
        getAnnouncements();
        getDiagnostics();
        getSoftware();

        $(".content").slimscroll({
            height: window.innerHeight - 135,
            alwaysVisible: true,
            size: "3px"
        });

        resetScrollBar();
    };

    function resetScrollBar() {
        $(".content").slimscroll({ scrollBy: 0 }); 
    };

    function getScheduledSlideshows() {
        var schedulesSlideShows = scheduledSlideshowService.query({ "signboardId": $routeParams.id }).
            $promise.then(function (scheduledSlideshows) {
                $scope.scheduledSlideshows = scheduledSlideshows;

                angular.forEach(scheduledSlideshows, (scheduledSlideshow, index) => {
                    slideshowService.get({ "slideshowId": scheduledSlideshow.slideshowId }).
                        $promise.then(
                        function success (data) {
                            scheduledSlideshow.name = data.name;
                            resetScrollBar();
                        },
                        function error(error) {
                            errorHandler.handleError(error, {}, "Unable to get scheduled slideshow name");
                        });
                });
            },
            function error(error) {
                errorHandler.handleError(error, {}, "Unable to get scheduled slideshows");
            });
    };

    function getAnnouncements() {
        announcementService.query().
            $promise.then(function (announcements: any[]) {
                $scope.announcements = $filter("filter")(announcements, { signboardIds: $scope.signboardId });
                $(".slim-announcements").slimscroll({
                    height: '300px',
                    size: '3px'
                });
                resetScrollBar();
            },
            function error(error) {
                errorHandler.handleError(error, {}, "Unable to get announcements for this signboard");
            });
    };

    function getDiagnostics() {
        diagnosticService.latest({ "signboardId": $routeParams.id }).
            $promise.then(
                function success (diagnostics: any[]) {
                    setTimeout(function () {
                        $scope.diagnostics = diagnostics;
                        $scope.hardwareResults = true;
                        setTimeout(function () { $scope.$apply(); }, 0);
                        setTimeout(function () { getDiagnostics() }, 30000);
                    }, 0);
                    resetScrollBar();
            },
            function error(error) {
                errorHandler.handleError(error, {}, "Unable to get diagnostics for signboard " + $routeParams.id);
                $scope.hardwareResults = true;
            });
    };

    function getSlideshows() {
        slideshowService.query().
            $promise.then(
            function success(slideshows) {
                $scope.slideshows = slideshows;
                resetScrollBar();
            },
            function error(error) {
                errorHandler.handleError(error, {}, "Unable to get slideshows");
            });
    };

    function screenSaverState() {

    };

    function getSoftware() {
        softwareService.query({}, function success(software) {
            $scope.softwares = software;
        }, function error(error) {
                errorHandler.handleError(error);
            });
    };

    $scope.$on('emit:dateTimePicker', function (e, value) {
        if (typeof ($scope.scheduledSlideshow) === "undefined") {
            $scope.scheduledSlideshow = {};
        }
        setTimeout(function () {
            $scope.scheduledSlideshow.startDate = new Date(value.dateTime._d);
            $scope.$apply();
        }, 0);
    });

    $scope.deploySoftware = function () {
        $scope.deployingSoftware = true;
        signboardsService.request({
            signboardId: $scope.signboardId,
            requestType: "SoftwareUpdate",
            value: $scope.selectedSoftwareVersion.softwareId
        }, function success() {
                $scope.deployingSoftware = false;
                toastr.info("Software deployment initiated", "Success");
            }, function error(error) {
                errorHandler.handleError(error);
                $scope.deployingSoftware = false;
            });
    };

    $scope.removeAnnouncement = function (announcement) {
        var modalInstance = $modal.open({
            templateUrl: "deleteModal.html",
            controller: "confirmationModal",
            size: "",
            resolve: {
            }
        });

        modalInstance.result.then(function (confirmation) {
            announcement.removing = true;
            $scope.removingAnnouncement = true;
            assignedSignboardsService.query({ announcementId: announcement.announcementId }).
                $promise.then(function (assignedSignboards) {
                angular.forEach(assignedSignboards,(signboard, index) => {
                    if (signboard.signboardId === $routeParams.id) {
                        assignedSignboards.splice(index, 1);
                        assignedSignboardsService.update({ announcementId: announcement.announcementId }, assignedSignboards,
                            function success() {
                                getAnnouncements();
                                $scope.removingAnnouncement = false;
                            },
                            function error(error) {
                                errorHandler.handleError(error);
                                $scope.removingAnnouncement = false;
                                announcement.removing = false;
                            });
                    }
                });
            }, errorHandler.handleError);
        });
    };

    $scope.scheduleSlideshow = function (form) {
        if (form.$invalid) {
            return false;
        }

        if (typeof ($scope.scheduledSlideshow.slideshowId) !== "undefined") {
            $scope.schedulingSlideshow = true;
            new scheduledSlideshowService({ slideshowId: $scope.scheduledSlideshow.slideshowId, startDate: new Date($scope.scheduledSlideshow.startDate).toISOString() }).
                $save({ signboardId: $routeParams.id },
                function success() {
                    getScheduledSlideshows();
                    $scope.schedulingSlideshow = false;
                    $scope.scheduledSlideshow = {};
                    $("#startDate").val("");
                },
                function error(error) {
                    $scope.schedulingSlideshow = false;
                    errorHandler.handleError(error, {}, "Unable to schedule slideshow");
                });
        }
    };

    $scope.unscheduleSlideshow = function (slideshow) {
        var modalInstance = $modal.open({
            templateUrl: "deleteModal.html",
            controller: "confirmationModal",
            size: "",
            resolve: {
            }
        });

        modalInstance.result.then(function (confirmation) {
            $scope.removingSchedule = true;
            slideshow.deleting = true;
            slideshow.$delete({ signboardId: $routeParams.id, slideshowId: slideshow.slideshowId, scheduledSlideshowId: slideshow.scheduledSlideshowId },
                function success() {
                    $scope.removingSchedule = false;
                    getScheduledSlideshows();
                },
                function error(error) {
                    delete slideshow.deleting;
                    $scope.removingSchedule = false;
                    errorHandler.handleError(error, {}, "Unable to get unscheduled slideshow");
                });
        });
    };

    $scope.setFontSizeFactor = function (form) {
        if (form.$invalid) {
            return false;
        }
        var signboardRequest = new signboardsService({ requestType: "FontFactorUpdate", value: $scope.fontSizeFactor });
        $scope.settingFontFactor = true;
        signboardRequest.$request({ signboardId: $routeParams.id },
            function success(requests) {
                $scope.settingFontFactor = false;
                delete $scope.fontSizeFactor;
                toastr.info("Font size factor changed");
            },
            function error(error) {
                $scope.settingFontFactor = false;
                errorHandler.handleError(error, {}, "Unable to set the font size factor");
            });
    };

    $scope.showScreenSaver = function () {
        var signboardRequest = new signboardsService({ requestType: "ShowScreensaver" });
        $scope.showingScreenSaver = true;
        signboardRequest.$request({ signboardId: $routeParams.id },
            function success(requests) {
                $scope.showingScreenSaver = false;
                toastr.info("Request sent", "Screensaver");
            },
            function error(error) {
                $scope.showingScreenSaver = false;
                errorHandler.handleError(error, {}, "Unable to send request");
            });
    };

    $scope.restartDevice = function () {
        var sr = new signboardsService({ requestType: "RestartDevice" });
        $scope.restartingDevice = true;
        sr.$request({ signboardId: $routeParams.id },
            function success(requests) {
                $scope.restartingDevice = false;
                toastr.info("Request set", "Restart");
            },
            function error(error) {
                $scope.restartingDevice = false;
                errorHandler.handleError(error, {}, "Unable to restart device");
            });
    };

    $scope.takeScreenshot = function () {
        var sr = new signboardsService({ requestType: "TakeScreenshot" });
        $scope.takingScreenshot = true;
        sr.$request({ signboardId: $routeParams.id },
            function success(requests) {
                $scope.takingScreenshot = false;
                toastr.info("Request sent", "Screenshot");
            },
            function error(error) {
                $scope.takingScreenshot = false;
                errorHandler.handleError(error, {}, "Unable to send request");
            });
    };

    $scope.getDiagnostic = function (metricName) {
        var returnVal = 0;
        if (typeof $scope.diagnostics === "undefined" || typeof $scope.diagnostics.metrics === "undefined") {
            return 0;
        }
        angular.forEach($scope.diagnostics.metrics,(metric, key) => {
            if (metric.name == metricName) {
                returnVal = metric.value;
            }
        });

        return returnVal;
    };

    $scope.showPreviousRequests = function () {
        var modalInstance = $modal.open({
            templateUrl: "requestModal.html",
            controller: "requestModal",
            size: "",
            resolve: {
                signboardId: function () {
                    return $scope.signboardId;
                }
            }
        });

        modalInstance.result.then(function (confirmation) {
            console.log(confirmation);
        });
    };
});
app.controller("confirmationModal", function ($scope, $modalInstance) {
    $scope.ok = function () {
        $modalInstance.close(true);
    };

    $scope.cancel = function () {
        $modalInstance.dismiss("cancel");
    };
});
app.controller("requestModal", function ($scope, $modalInstance, signboardsService, signboardId, errorHandler) {
    $scope.loadingData = true;
    getRequests();

    function getRequests() {
        signboardsService.request({
            signboardId: signboardId
        }, function success(signboards) {
            }, function error(error) {
                errorHandler.handleError(error);
            });
    }

    $scope.ok = function () {
        $modalInstance.dismiss("cancel");
    };
});