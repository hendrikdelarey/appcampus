/// <reference path="../app.ts" />
app.factory("slideshowService", function ($resource, userService, ENV) {
    var requestUri = ENV.apiUrl + "/api/v1/companies/:companyId/slideshows/:slideshowId";
    return $resource(requestUri, { companyId: function () { return userService.getCompanyId(); } }, {
        'save': { method: 'POST' },
        'update': { method: 'PUT' },
        'move': { method: 'PATCH' }
    });
});
