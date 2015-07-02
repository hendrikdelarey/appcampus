/// <reference path="../app.ts" />
app.factory("scheduledSlideshowService", function ($resource, userService, ENV) {
    var requestUri = ENV.apiUrl + "/api/v1/companies/:companyId/signboards/:signboardId/scheduledSlideshows/:scheduledSlideshowId";
    return $resource(requestUri, { companyId: function () { return userService.getCompanyId(); } }, {
        'save': { method: 'POST' },
    });
});
