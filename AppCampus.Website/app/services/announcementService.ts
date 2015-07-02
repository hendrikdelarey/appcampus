/// <reference path="../app.ts" />
app.factory("announcementService", function ($resource, userService, ENV) {
    var requestUri = ENV.apiUrl + "/api/v1/companies/:companyId/announcements/:announcementId";
    return $resource(requestUri,
        { companyId: function () { return userService.getCompanyId() } },
        {
            update: { method: "PUT" },
        });
});
