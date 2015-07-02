/// <reference path="../app.ts" />
app.factory("widgetService", function ($resource, userService, ENV) {
    var requestUri = ENV.apiUrl + "/api/v1/companies/:companyId/slideshows/:slideshowId/slides/:slideId/widgets/:widgetId";
    return $resource(requestUri, { companyId: userService.getCompanyId() }, {
        'save': { method: 'POST' },
    });
});
app.factory("widgetDefinitionService", function ($resource, userService, ENV) {
    var requestUri = ENV.apiUrl + "/api/v1/widgetDefinitions/:widgetDefinitionId";
    return $resource(requestUri, {}, {
        'save': { method: 'POST' },
    });
});
app.factory("parameterService", function ($resource, userService, ENV) {
    var requestUri = ENV.apiUrl + "/api/v1/companies/:companyId/slideshows/:slideshowId/slides/:slideId/widgets/:widgetId/parameters/:parameterName";
    return $resource(requestUri, { companyId: userService.getCompanyId() }, {
        'save': { method: 'PUT' },
    });
});
app.factory("parameterDefinitionService", function ($resource, userService, ENV) {
    var requestUri = ENV.apiUrl + "/api/v1/widgetDefinitions/:widgetDefinitionId/parameterDefinitions/:parameterName";
    return $resource(requestUri);
});
app.factory("stopsDefinitionService", function ($resource, userService, ENV) {
    var requestUri = ENV.apiUrl + "/api/v1/widgetcontent/stops";
    return $resource(requestUri);
});
app.factory("imageService", function ($resource, userService, ENV) {
    var requestUri = ENV.apiUrl + "/api/v1/widgetcontent/images/:imageId";
    return $resource(requestUri);
});
app.factory("operatorService", function ($resource, ENV) {
    var requestUri = ENV.apiUrl + "/api/v1/widgetcontent/operators";
    return $resource(requestUri);
});
