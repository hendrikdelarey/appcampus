app.controller("slideshowsController", function ($scope, $log, $modal, $location, devicesService, slideshowService, slideService, errorHandler) {
    init();
    function init() {
        loadSlideshows();
        $(".content").slimscroll({
            height: window.innerHeight - 135,
            alwaysVisible: true,
            size: "3px"
        });
    }
    ;
    function loadSlideshows() {
        slideshowService.query().
            $promise.then(function (slideshows) {
            $scope.slideshows = slideshows;
        }, errorHandler.handleError);
    }
    ;
    $scope.create = function () {
        var modalInstance = $modal.open({
            templateUrl: 'myModalContent.html',
            controller: 'slideshowCreateModal',
            size: "",
            resolve: {}
        });
        modalInstance.result.then(function (newName) {
            $scope.slideshowName = newName;
            var slideshow = new slideshowService({ "Name": $scope.slideshowName });
            slideshow.$save({}, function success(data) {
                $location.path("/slideshows/" + slideshow.slideshowId);
            }, function error(error) {
                errorHandler.handleError(error);
            });
        });
    };
    $scope.deleteSlideshow = function (slideshow) {
        var modalInstance = $modal.open({
            templateUrl: "deleteModal.html",
            controller: "slideshowDeleteModal",
            size: "",
            resolve: {}
        });
        modalInstance.result.then(function (confirmation) {
            $scope.removingSlideshow = true;
            slideshow.removing = true;
            slideshow.$delete({ slideshowId: slideshow.slideshowId }, function (deletedSlideshow) {
                loadSlideshows();
                $scope.removingSlideshow = false;
            }, function (error) {
                $scope.removingSlideshow = false;
                slideshow.removing = false;
                errorHandler.handleError(error);
            });
        });
    };
});
app.controller("slideshowController", function ($scope, $log, $routeParams, $modal, $location, toastr, slideshowService, slideService, widgetService, widgetDefinitionService, parameterDefinitionService, parameterService, errorHandler) {
    $scope.slides = [];
    $scope.alreadyHasWidget = false;
    init();
    function init() {
        loadSlideShow();
        loadSlides();
        loadWidgetDefinitions();
        $(".content").slimscroll({
            height: window.innerHeight - 135,
            alwaysVisible: true,
            size: "3px"
        });
    }
    ;
    function resetScrollBar() {
    }
    ;
    function loadSlideShow() {
        slideshowService.get({ slideshowId: $routeParams.id }).
            $promise.then(function (slideshow) {
            $scope.slideshowName = slideshow.name;
            $scope.slideshow = slideshow;
            resetScrollBar();
        }, function (data) {
            errorHandler.handleError(data, function () { $location.path("/slideshows"); }, "Slideshow does not exist");
        });
    }
    ;
    function loadSlides() {
        slideService.query({ slideshowId: $routeParams.id }).
            $promise.then(function success(slides) {
            $scope.slides = slides;
            resetScrollBar();
            $scope.actionHappening = false;
            $scope.slideActivity = false;
        }, function error(error) {
            errorHandler.handleError(error);
            $scope.actionHappening = false;
            $scope.slideActivity = false;
        });
    }
    ;
    function loadWidgetDefinitions() {
        widgetDefinitionService.query().
            $promise.then(function (widgetDefinitions) {
            $scope.widgetDefinitions = widgetDefinitions;
        }, errorHandler.handleError);
    }
    ;
    function loadWidgets(slide) {
        $scope.alreadyHasWidget = false;
        widgetService.query({ slideshowId: $routeParams.id, slideId: slide.slideId }).
            $promise.then(function (widget) {
            $scope.actionHappening = false;
            $scope.slideActivity = false;
            if (widget.length > 0) {
                $scope.alreadyHasWidget = true;
                $scope.selectedSlideWidget = widget[0];
                loadParameters();
                resetScrollBar();
            }
            else {
                $scope.selectedSlideWidget = "";
            }
        });
    }
    ;
    function loadParameters() {
        $scope.parametersSet = true;
        $scope.parametersLoading = true;
        parameterDefinitionService.query({ widgetDefinitionId: $scope.selectedSlideWidget.widgetDefinitionId }).
            $promise.then(function (parameterDefinitions) {
            $scope.parameterDefinitions = parameterDefinitions;
            var completedCall = 0;
            angular.forEach($scope.parameterDefinitions, function (value, key) {
                parameterService.get({ slideshowId: $routeParams.id, slideId: $scope.selectedSlide.slideId, widgetId: $scope.selectedSlideWidget.widgetId, parameterName: value.name }).
                    $promise.then(function (parameter) {
                    value["value"] = parameter.value;
                    resetScrollBar();
                    completedCall++;
                    if (completedCall === $scope.parameterDefinitions.length) {
                        $scope.parametersLoading = false;
                    }
                }, function (error) {
                    if (error.status === 404 || error.status === 0) {
                        $scope.parametersSet = false;
                        value["value"] = "Not Set";
                    }
                    completedCall++;
                    if (completedCall === $scope.parameterDefinitions.length) {
                        $scope.parametersLoading = false;
                    }
                });
            });
        });
    }
    ;
    function findOperatorName() {
        var returnValue = "Not Set";
        angular.forEach($scope.parameterDefinitions, function (pdef, key) {
            if (pdef.name.indexOf("OperatorName") >= 0) {
                returnValue = pdef.value;
            }
        });
        return returnValue;
    }
    ;
    $scope.updateslideshowName = function (form) {
        if (form.$invalid) {
            return false;
        }
        $scope.updatingSlideshowName = true;
        $scope.slideshow.$update({ slideshowId: $scope.slideshow.slideshowId }, function success() {
            $scope.updatingSlideshowName = false;
            $scope.slideshowName = $scope.slideshow.name;
            toastr.success("Slideshow name updated", "Success");
        }, function error(error) {
            $scope.updatingSlideshowName = false;
            errorHandler.handleError(error);
        });
    };
    $scope.createNewSlide = function (slidesForm) {
        if (slidesForm.$invalid) {
            return false;
        }
        if (typeof ($scope.newSlideName) !== "undefined" && $scope.newSlideName.trim() !== "") {
            $scope.createNewSlideLoader = true;
            var slide = new slideService({ name: $scope.newSlideName, backgroundColourHexCode: "#ffffff", transitionType: "None", durationInSeconds: 15, isActive: false });
            slide.$save({ slideshowId: $routeParams.id }, function success(slide) {
                $scope.createNewSlideLoader = false;
                $scope.newSlideName = "";
                loadSlides();
            }, function (error) {
                $scope.createNewSlideLoader = false;
                errorHandler.handleError();
            });
        }
        ;
    };
    $scope.editSlide = function (slide) {
        $log.log(slide);
        $scope.slideDetailsVisible = true;
        $scope.actionHappening = true;
        $scope.slideActivity = true;
        $scope.alreadyHasWidget = false;
        loadWidgets(slide);
        $scope.selectedSlide = slide;
    };
    $scope.moveSlideUp = function (index, slide) {
        if (index - 1 >= 0) {
            $scope.slideActivity = true;
            $scope.slideshow.operation = 'MoveAfter';
            $scope.slideshow.referencedSlideId = $scope.slides[index - 1].slideId;
            $scope.slideshow.slideId = slide.slideId;
            $scope.actionHappening = true;
            $scope.slideActivity = true;
            $scope.slideshow.$move({ slideshowId: $routeParams.id }, function success(updatedSlide) {
                $log.log(updatedSlide);
                loadSlides();
            }, function error(error) {
                errorHandler.handleError(error);
                $scope.actionHappening = false;
                $scope.slideActivity = false;
            });
        }
    };
    $scope.moveSlideDown = function (index, slide) {
        if (index + 1 < $scope.slides.length) {
            $scope.slideActivity = true;
            $scope.slideshow.operation = 'MoveBefore';
            $scope.slideshow.referencedSlideId = $scope.slides[index + 1].slideId;
            $scope.slideshow.slideId = slide.slideId;
            $scope.actionHappening = true;
            $scope.slideActivity = true;
            $scope.slideshow.$move({ slideshowId: $routeParams.id }, function success(updatedSlide) {
                loadSlides();
            }, function error() {
                errorHandler.handleError();
                $scope.actionHappening = false;
                $scope.slideActivity = false;
            });
        }
    };
    $scope.removeSlide = function (slide, callback) {
        var modalInstance = $modal.open({
            templateUrl: "deleteModal.html",
            controller: "slideshowDeleteModal",
            size: "",
            resolve: {}
        });
        modalInstance.result.then(function (confirmation) {
            $scope.slideActivity = true;
            var hideParametersWindow = false;
            if (slide === $scope.selectedSlide) {
                hideParametersWindow = true;
            }
            slide.deleting = true;
            $scope.actionHappening = true;
            slide.$delete({ slideshowId: $routeParams.id, slideId: slide.slideId }, function success() {
                $scope.alreadyHasWidget = false;
                $scope.slideDetailsVisible = false;
                if (typeof (callback) === "function") {
                    callback();
                }
                else {
                    loadSlides();
                }
            }, function error(error) {
                errorHandler.handleError(error);
                $scope.slideActivity = false;
                $scope.actionHappening = false;
                slide.deleting = false;
            });
        });
    };
    $scope.updateSlideDetails = function () {
        $scope.updatingWidgetParameters = true;
        $scope.selectedSlide.$update({ slideshowId: $routeParams.id, slideId: $scope.selectedSlide.slideId }, function success() {
            $scope.updatingWidgetParameters = false;
            if (!$scope.alreadyHasWidget && typeof ($scope.selectedSlideWidget) !== "undefined") {
                $scope.selectedSlideWidget = new widgetService({ widgetDefinitionId: $scope.selectedSlideWidget.widgetDefinitionId });
                $scope.selectedSlideWidget.$save({ slideshowId: $routeParams.id, slideId: $scope.selectedSlide.slideId }, function (widget) {
                    $scope.alreadyHasWidget = true;
                    loadParameters();
                }, function (error) {
                    errorHandler.handleError(error);
                });
            }
        }, function error(error) {
            $scope.updatingWidgetParameters = false;
            errorHandler.handleError(error);
        });
    };
    $scope.editParameter = function (parameterName, parameterDefinitionValue) {
        var operatorName = "";
        if (parameterDefinitionValue === "Not Set") {
            parameterDefinitionValue = "";
        }
        if (parameterName === "StopIdentifier") {
            if (findOperatorName() == "Not Set") {
                toastr.warning("You must select an operator before you can select a stop");
                return false;
            }
            else {
                angular.forEach($scope.parameterDefinitions, function (parameter, index) {
                    if (parameter.name.trim() === "OperatorName") {
                        operatorName = parameter.value;
                    }
                });
            }
        }
        var modalInstance = $modal.open({
            templateUrl: 'myModalContent.html',
            controller: 'updateParameterModal',
            size: "",
            resolve: {
                parameterName: function () {
                    return parameterName;
                },
                parameterDefinitionValue: function () {
                    return parameterDefinitionValue;
                },
                operatorName: function () {
                    return operatorName;
                }
            }
        });
        modalInstance.result.then(function (newName) {
            var param = new parameterService({
                parameterName: parameterName,
                value: newName
            });
            $scope.parametersLoading = true;
            param.$save({ slideshowId: $routeParams.id, slideId: $scope.selectedSlide.slideId, widgetId: $scope.selectedSlideWidget.widgetId }, function success(param) {
                $scope.parametersLoading = false;
                loadParameters();
            }, function (error) {
                $scope.parametersLoading = false;
                errorHandler.handleError(error);
            });
        }, function () {
        });
    };
});
app.controller("slideshowCreateModal", function ($scope, $timeout, $modalInstance) {
    $scope.slideshowName = "";
    $scope.ok = function () {
        $scope.slideshowName = $scope.slideshowName.trim();
        if ($scope.slideshowName !== "") {
            $modalInstance.close($scope.slideshowName);
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
app.controller("updateParameterModal", function ($scope, $timeout, $log, $modalInstance, toastr, stopsDefinitionService, parameterName, parameterDefinitionValue, operatorName, imageService, operatorService, errorHandler) {
    $scope.parameterName = parameterName;
    $scope.parameterNameValue = parameterDefinitionValue;
    $scope.operator = operatorName;
    console.log(parameterDefinitionValue);
    console.log($scope.parameterNameValue);
    switch ($scope.parameterName) {
        case "StopIdentifier":
            $scope.loadingData = true;
            $scope.stops = stopsDefinitionService.query({}, function (stops) {
                $scope.stops = stops;
                $scope.loadingData = false;
                setTimeout(function () { $scope.$apply(); }, 0);
            }, function (error) {
                errorHandler.handleError(error);
                $scope.loadingData = false;
            });
            break;
        case "OperatorName":
            $scope.loadingData = true;
            $scope.operators = operatorService.query({}, function (operators) {
                $scope.operators = operators;
                $scope.loadingData = false;
                setTimeout(function () { $scope.$apply(); }, 0);
            }, function (error) {
                $scope.loadingData = false;
            });
            break;
    }
    ;
    $scope.saveParameter = function (value) {
        if ($scope.parameterNameValue !== "") {
            $modalInstance.close($scope.parameterNameValue);
        }
        else {
            $scope.warningText = "You need to specify a value";
            $timeout(function () { $scope.warningText = ""; }, 2000);
        }
    };
    $scope.ok = function (form) {
        if (form.$invalid) {
            return false;
        }
        if (typeof ($scope.image) !== "undefined" && typeof ($scope.image.filetype) !== "undefined" && $scope.image.filetype.indexOf("image") !== -1) {
            var image = new imageService({
                "base64Image": $scope.image.base64,
                "name": $scope.image.filename
            });
            image.$save({}, function success(image) {
                toastr.info("Image uploaded");
                $scope.parameterNameValue = image.imageId;
                $scope.saveParameter();
            }, errorHandler.handleError);
        }
        else {
            if (typeof ($scope.parameterNameValue) !== "number") {
                $scope.parameterNameValue = $scope.parameterNameValue.trim();
            }
            $scope.saveParameter();
        }
    };
    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
});
app.controller("slideshowDeleteModal", function ($scope, $modalInstance) {
    $scope.ok = function () {
        $modalInstance.close(true);
    };
    $scope.cancel = function () {
        $modalInstance.dismiss("cancel");
    };
});
