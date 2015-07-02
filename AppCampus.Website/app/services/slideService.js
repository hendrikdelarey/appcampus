/// <reference path="../app.ts" />
app.factory("slideService", function ($resource, userService, ENV) {
    var requestUri = ENV.apiUrl + "/api/v1/companies/:companyId/slideshows/:slideshowId/slides/:slideId";
    return $resource(requestUri, { companyId: function () { return userService.getCompanyId(); } }, {
        'save': { method: 'POST' },
        'update': { method: 'PUT' }
    });
});
