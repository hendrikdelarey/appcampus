app.factory("assignedSignboardsService", function ($resource, userService, ENV) {
    var requestUri = ENV.apiUrl + "/api/v1/companies/:companyId/announcements/:announcementId/assignedsignboards";
    return $resource(requestUri, { companyId: function () { return userService.getCompanyId(); } }, {
        save: { method: 'POST' },
        update: { method: 'PUT', isArray: true }
    });
});
