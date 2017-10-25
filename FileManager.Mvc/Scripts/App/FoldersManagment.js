
fileManagerApp.controller("folderController", ['$scope', '$http','$state','upload', 'filesData', function ($scope, $http, $state, upload, filesData)
{
    $scope.folder = {};
    $scope.folder.files = [];
    $scope.folder.folderId = 0;
    $scope.showNewFileUploadForm = false;
    $scope.showEmailForm = false;
    $scope.showOrEditFileComments = false;
    $scope.emailModel = {};
    $scope.emailModel.RecipientsEmail = "";
    $scope.actionResult = "";

    $scope.openFileUploadForm = function () {
        $scope.showNewFileUploadForm = !$scope.showNewFileUploadForm;
    }

    $scope.emailForm = function (file) {
        $scope.showEmailForm = !$scope.showEmailForm;
        $scope.emailModel.FileId = file.FileId;
        $scope.emailModel.Subject = file.FileName;
    }

    $scope.showComments = function (file) {
        $scope.emailModel.RecipientsEmail = !$scope.emailModel.RecipientsEmail;
    }

    $scope.sendMail = function (recipientEmail) {
        filesData.SendEmail($scope.emailModel).then(function () {
            $scope.emailModel.RecipientsEmail = "";
            $scope.emailModel = {};
            $scope.showEmailForm = !$scope.showEmailForm;
            $scope.actionResult = "Mail was sent";
        })   
    }

    $scope.uploadFile = function () {
        upload({
            url: '/FilesManagment/UploadFile',
            method: 'POST',
        })
    }

    $scope.onComplete = function () {
        UpdateFiles();
    }

    $scope.downloadFile = function (file) {
        window.open('/FilesManagment/DownloadFile/?fileid=' + file.FileId)
    }

    Init();

    function Init() {
        $scope.folder.name = $state.params.folderName;
        $scope.folder.folderId = parseInt($state.params.folderId);
        UpdateFiles();
    }
    
    function UpdateFiles() {
        filesData.GetAllFilesFromFolder($scope.folder).then(function (val) {
            $scope.folder.files = val.data;
        })
    }
}])


fileManagerApp.factory('filesData', ["$http", "$rootScope", function ($http, $rootScope) {

    var post = $http.post,
               get = $http.get,
               baseUrl = "/FilesManagment/";

    return {
        GetAllFilesFromFolder: function (folder) {           
            return post(baseUrl + 'GetAllFilesFromFolder', folder);
        },
        SendEmail: function (model) {           
            return post(baseUrl + 'SendFileByEmail', model);
        }
    }
}])

