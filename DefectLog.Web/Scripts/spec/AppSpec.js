describe("defectLog", function () {

    beforeEach(module('defectLog'));
    
    window.common = {
        baseUrl: '',
        dataStore: {}
    };

    describe("DefectCtrl", function () {
        var scope, httpBackend;
        
        beforeEach(inject(function ($rootScope, $controller, $httpBackend) {
            scope = $rootScope.$new();
            httpBackend = $httpBackend;

            $controller("DefectCtrl", {
                $scope: scope,
                $httpBacked: httpBackend
            });

            scope.scrollToForm = new Function();

            scope.formValid = function () {
                return true;
            };

            scope.resetValidator = new Function();
        }));

        it('should not attempt to load defects when versionId is empty', function() {
            scope.versionId = undefined;
            scope.loadDefects();
            expect(scope.defects).not.toBeNull();
        });

        it('should load defect when defect is clicked', function () {
            scope.defectClicked({ prop: 1 });
            expect(scope.defect.prop).toBe(1);
        });

        it('should clear defect after saving', function () {
            httpBackend.whenPOST('api/defect').respond({});
            scope.form = {};
            
            scope.saveDefect();
            httpBackend.flush();

            expect(scope.form).toBeNull();
        });

        it('should clear form when closeForm is called', function() {
            scope.form = {};
            scope.closeForm();

            expect(scope.form).toBeNull();
        });
    });
});