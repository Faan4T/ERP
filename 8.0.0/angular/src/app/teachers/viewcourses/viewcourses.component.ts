import { AppComponentBase } from '@shared/app-component-base';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { CourseDto, StudentServiceProxy, StudentWithCourse, TeacherServiceProxy, TeacherWithCourse } from './../../../shared/service-proxies/service-proxies';
import { Component, Injector, OnInit } from '@angular/core';

@Component({
  selector: 'viewcourses',
  templateUrl: './viewcourses.component.html',
  styles: [
  ]
})
export class ViewcoursesComponent extends AppComponentBase implements OnInit {
  saving = false;
  teachercourses: TeacherWithCourse[]=[]
  course : CourseDto[];
  id: number;
  courseid: number;
  rollno:string;
  result: any;
  i:[];
  constructor(
    injector: Injector,
    public _teacherService: TeacherServiceProxy,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    debugger
    this._teacherService.getCourses(this.id).subscribe((result) => {debugger
      this.teachercourses= result;
      this.result = result;
       });
  }

  DropCourse(teacher: TeacherWithCourse): void {
   debugger
    this.showDropCourseDialog(teacher.courseId,teacher.teacherId,teacher.courses.courseName) ;
  }
  showDropCourseDialog(courseId?: any,rollno?: any,courseName?:any): void {
    if (courseId != null) {
      debugger
          abp.message.confirm(
        this.l('Are You sure To drop This Course'+"( "+ courseName +" )"),
        undefined,
        (result: boolean) => {
          if (result) {
            debugger
            this._teacherService.dropCourse(courseId,rollno).subscribe(() => {
              abp.notify.success(this.l('Dropped Successfully '));
              this._teacherService.getCourses(this.id).subscribe((result) => {
                this.teachercourses= result;
            });
            });
          }

        }
      );

    }


  }

  DropAllCourse(): void {
  debugger
    this.showDropAllCourseDialog(this.id, this.result);
  }
  showDropAllCourseDialog(id?: any,courseId?:any): void {
  debugger
  if(courseId!=0)
  {
      abp.message.confirm(
        this.l('Are You sure To drop All Course'),
        undefined,
        (result: boolean) => {
          debugger;
          if (result==true) {
            debugger
            this._teacherService.dropAllCourses(id).subscribe(() => {
              abp.notify.success(this.l('All Courses Dropped Successfully '));
              this._teacherService.getCourses(this.id).subscribe((result) => {
                this.teachercourses= result;
              });
            });

          }
        }
      );
  }
  else{
    abp.notify.warn(this.l('No Courses Enrolled'));
  }
  }
  }


