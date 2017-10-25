


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
    var fileModel = {};
    $scope.files = [];
    $scope.folders = [];
    var folderModel = {};
    $scope.folderName = "";
    $scope.showNewFolderName = false;
    $scope.showFolderRenameForm = false;

    Init();

    $scope.openNewFolderForm = function(){   
        $scope.showNewFolderName = !$scope.showNewFolderName;  
    }

    $scope.openFolderEditForm = function (folder) {
        $scope.showFolderRenameForm = !$scope.showFolderRenameForm;
        $scope.folderName = folder.FolderName;
    }

    $scope.createFolder = function () {
        if ($scope.folderName) {        
            folderModel.FolderName = $scope.folderName;
            foldersData.CreateNewFolder(folderModel).then(function (){
                UpdateFolders()
            })
        }
        $scope.folderName = "";
        $scope.showNewFolderName = !$scope.showNewFolderName;
    }

    $scope.deleteFolder = function (model) { 
        foldersData.DeleteFolder(model).then(function () {
            UpdateFolders()
        });   
    }


    $scope.renameFolder = function () {
        $scope.showFolderRenameForm = !$scope.showFolderRenameForm;
        folderModel.FolderName = $scope.folderName;
        foldersData.UpdateFolder(model).then(function () {
            UpdateFolders();
        })
        $scope.folderName = "";        
        console.log("Rename");
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
        CreateNewFolder: function (model) {
            return post(foldersManagerUrl + "CreateNewFolder", model);
        },

        GetAllFolders: function () {
            return get(baseUrl + "GetAllFolders");
        },

        DeleteFolder: function (model) {
            return post(foldersManagerUrl + "DeleteFolder", model);
        },

        UpdateFolder: function (model) {
            return post(foldersManagerUrl + "UpdateFolder", model);
        }
    }
}])






