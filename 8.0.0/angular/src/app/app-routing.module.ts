import { TeachersComponent } from './teachers/teachers.component';
import { TeacherTreeViewComponent } from './teachers/teacher-tree-view/teacher-tree-view.component';
import { TeacherTreeViews } from './../shared/service-proxies/service-proxies';
import { DepartmentsComponent } from './departments/departments.component';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { UsersComponent } from './users/users.component';
import { TenantsComponent } from './tenants/tenants.component';
import { RolesComponent } from 'app/roles/roles.component';
import { ChangePasswordComponent } from './users/change-password/change-password.component';
import { StudentsComponent } from './students/students.component';
import { TreeViewComponent } from './tree-view/tree-view.component';
import { CoursesComponent } from './courses/courses.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: AppComponent,
                children: [
                    { path: 'home', component: HomeComponent,  canActivate: [AppRouteGuard] },
                    { path: 'users', component: UsersComponent, data: { permission: 'Pages.Users' }, canActivate: [AppRouteGuard] },
                    { path: 'roles', component: RolesComponent, data: { permission: 'Pages.Roles' }, canActivate: [AppRouteGuard] },
                    { path: 'students', component: StudentsComponent, data: { permission: 'Pages.Students' }, canActivate: [AppRouteGuard] },
                    { path: 'departments', component: DepartmentsComponent, data: { permission: 'Pages.Departments' }, canActivate: [AppRouteGuard] },
                    { path: 'courses', component: CoursesComponent, data: { permission: 'Pages.Courses' }, canActivate: [AppRouteGuard] },
                    { path: 'tree-view', component: TreeViewComponent, data: { permission: null }, canActivate: [AppRouteGuard] },
                    { path: 'teachers', component: TeachersComponent, data: { permission: 'Pages.Teachers' }, canActivate: [AppRouteGuard] },
                    { path: 'teacher-tree-view', component: TeacherTreeViewComponent, data: { permission: null }, canActivate: [AppRouteGuard] },
                    { path: 'tenants', component: TenantsComponent, data: { permission: 'Pages.Tenants' }, canActivate: [AppRouteGuard] },
                    { path: 'about', component: AboutComponent, canActivate: [AppRouteGuard] },
                    { path: 'update-password', component: ChangePasswordComponent, canActivate: [AppRouteGuard] }
                ]
            }
        ])
    ],
    exports: [RouterModule]
})
export class AppRoutingModule { }
