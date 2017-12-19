
fileManagerApp.controller("folderController", ['$scope', '$http','$state','upload', 'filesData', function ($scope, $http, $state, upload, filesData)
{
    
    $scope.folder = {};
    $scope.folder.name = "";
    $scope.files = [];
    $scope.file = {};
    $scope.file.FileComments = [];
    $scope.file.FileNameChanges = [];
    $scope.showNewFileUploadForm = false;
    $scope.showEmailForm = false;
    $scope.showFileRenameForm = false;
    $scope.newComment = "";
    $scope.emailModel = {};
    $scope.emailModel.RecipientsEmail = "";
    $scope.actionResult = "";
    

    $scope.openFileUploadForm = function () {    
        $scope.showNewFileUploadForm = !$scope.showNewFileUploadForm;
    }

    $scope.emailForm = function (file) {        
        $scope.emailModel.FileId = file.FileId;
        $scope.emailModel.Subject = file.FileName;
    }

    $scope.FileRenameForm = function (val) {       
        $scope.showFileRenameForm = !$scope.showFileRenameForm;
        $scope.file = val;
    }

    $scope.showComments = function (file) {
        $scope.noComments = "";
        filesData.GetFileComments(file).then(function (result) {
            $scope.file.FileId = file.FileId;
            if (result.data.length == 0) {
                $scope.noComments = "No Comments";
                $scope.file.FileComments = [];
            }
            else {
                $scope.file.FileComments = result.data;
                $scope.noComments = "";
            }
        })
    }

    $scope.showHistory = function (file) {
        $scope.noHistory = "";
        filesData.GetFileNameChangesHistory(file).then(function (result) {
            $scope.file.FileId = file.FileId;
            if (result.data.length == 0){
                $scope.noHistory = "No changes"
                $scope.file.FileNameChanges = [];
            }
            else {
                $scope.file.FileNameChanges = result.data;
                $scope.noHistory = "";
            }
        })
    }

    $scope.sendMail = function (recipientEmail) {
        jQuery("#emailForm").modal("hide");
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove();
        filesData.SendEmail($scope.emailModel).then(function () {
            $scope.emailModel.RecipientsEmail = "";
            $scope.emailModel = {};
            $scope.actionResult = "Mail was sent";
        }, function (error) {
            $scope.actionResult = error.statusText;
        })
    }

    $scope.uploadFile = function () {
        upload({
            url: '/FilesManagment/UploadFile',
            method: 'POST',
        })
    }

    $scope.renameFile = function (val) {
        jQuery("#filerenameForm").modal("hide");
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove();
        $scope.file.FileName = val;
        filesData.RenameFile($scope.file).then(function () {
            UpdateFiles()
        })
    }

    $scope.onComplete = function () {
        UpdateFiles();
    }

    $scope.downloadFile = function (file) {
        window.open('/FilesManagment/DownloadFile/?fileid=' + file.FileId)
    }

    $scope.AddCommetToFile = function (comment) {
        if (comment) {
            jQuery("#folderComments").modal("hide");
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $scope.file.FileComments.push(comment);
            $scope.file.LastComment = comment;
            filesData.UpdateFileComments($scope.file).then(function (result) {
            })
        }
    }

    Init();

    function Init() {
        $scope.folder.name = $state.params.folderName;
        $scope.folder.folderId = parseInt($state.params.folderId);
        UpdateFiles();
    }
    
    function UpdateFiles() {
        filesData.GetAllFilesFromFolder($scope.folder).then(function (val) {
            $scope.files = val.data;
        })
    }
}])


fileManagerApp.factory('filesData', ["$http", "$rootScope", function ($http, $rootScope) {

    var post = $http.post,
               get = $http.get,
               baseUrl = "/FilesManagment/";

    return {
        GetAllFilesFromFolder: function (folder) {           
            return get(baseUrl + 'GetAllFilesFromFolder', { params: { folderId: folder.folderId } });
        },
        SendEmail: function (model) {           
            return post(baseUrl + 'SendFileByEmail', model);
        },
        RenameFile: function (file) {
            return post(baseUrl + 'RenameFile', file);
        },
        GetFileComments: function (val) {
            return get(baseUrl + 'GetFileComments', { params: { fileid: val.FileId } });
        },
        UpdateFileComments: function (updatedFile) {
            return post(baseUrl + 'UpdateFileComments', updatedFile);
        },
        GetFileNameChangesHistory: function (val) {
            return get(baseUrl + 'GetNamesHistory', { params: { fileid: val.FileId } })
        }
    }
}])

