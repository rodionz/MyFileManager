


var fileManagerApp = angular.module('fileManagerApp', ['lr.upload','ui.router']);

fileManagerApp.config(["$stateProvider", "$urlRouterProvider", function ($stateProvider, $urlRouterProvider) {

    $urlRouterProvider.otherwise('/home');

    $stateProvider
    .state('home', {
        url: '/home/index',
        templateUrl: '/Home/Index',
        controller: 'mainController'
    })
    .state('singleFolderView', {
        url: '/singleFolderView/?folderId',
        templateUrl: '/FoldersManagment/Index',
        controller: 'folderController',
        params: {folderId : null , folderName: null}
    })
}]);

fileManagerApp.controller("mainController", function ($scope, foldersData){
    //var fileModel = {};
    $scope.files = [];
    $scope.folders = [];
    $scope.folderModel = {};
    $scope.folderModel.FolderName = "";
  
    Init();

    $scope.setFolder = function (folder) {      
        $scope.folderModel = folder;
    }

    $scope.createFolder = function (data) {
        if (data) {
            jQuery("#newFolderForm").modal("hide");
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $scope.folderModel.FolderName = data;
            foldersData.CreateNewFolder($scope.folderModel).then(function () {
                $scope.folderModel.FolderName = "";
                $scope.newName = "";
                UpdateFolders()
            })
        }       
    }

    $scope.deleteFolder = function (model) { 
        foldersData.DeleteFolder(model).then(function () {
            UpdateFolders()
        });   
    }


    $scope.renameFolder = function () {
        jQuery("#folderEditForm").modal("hide");
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove();
            foldersData.UpdateFolder($scope.folderModel).then(function () {
                $scope.showFolderRenameForm = !$scope.showFolderRenameForm;
                $scope.folderModel = {};
                UpdateFolders();
            })          
    }

    function Init() {
        UpdateFolders();
    }

    function UpdateFolders() {
        foldersData.GetAllFolders().then(function (val) {
            $scope.folders = val.data;           
        });
    }

});

fileManagerApp.factory('foldersData', ["$http", "$rootScope", function ($http, $rootScope) {

    var post = $http.post,
               get = $http.get,
               baseUrl = "/Home/",
               foldersManagerUrl = "/FoldersManagment/";

    return {
        CreateNewFolder: function (newFolder) {
            return post(foldersManagerUrl + "CreateNewFolder", newFolder);
        },

        GetAllFolders: function () {
            return get(baseUrl + "GetAllFolders");
        },

        DeleteFolder: function (model) {
            return post(foldersManagerUrl + "DeleteFolder", model);
        },

        UpdateFolder: function (updatedfolder) {
            return post(foldersManagerUrl + "UpdateFolder", updatedfolder);
        }
    }
}])






