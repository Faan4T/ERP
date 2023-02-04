import { Component, OnInit } from '@angular/core';
import { StudentDto, CourseDto, GetAllCoursesForRegisterDto, StudentServiceProxy } from '@shared/service-proxies/service-proxies';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'add-courses',
  templateUrl: './add-courses.component.html',
  styles: [
  ]
})
export class AddCoursesComponent implements OnInit {
  title?: string;
  closeBtnName?: string;
  onSave?: string;
  list: any[] = [];
  checkedIDs :any= [];
  student: StudentDto[];
  course: CourseDto[] = [];
  getcourseforRegister: GetAllCoursesForRegisterDto[]=[]
  selectedcourseforRegister: GetAllCoursesForRegisterDto[]=[]
  //student = new StudentDto();
  rollno: any;
  id: number;

  constructor(
    public bsModalRef: BsModalRef,
    private _studentService: StudentServiceProxy,
    ) {}

  ngOnInit() {
    debugger
    this._studentService
      .getRegisteredCourses(this.id)
      .subscribe((result)=>{
        this.getcourseforRegister = result
      })
  }

  changeSelection() {
    debugger
    this.fetchSelectedCourses()
    this.fetchCheckedCourseIDs()
  }

  fetchSelectedCourses() {
    debugger
    this.selectedcourseforRegister = this.getcourseforRegister.filter((value, index) => {
      return value.check
    });
  }

  fetchCheckedCourseIDs() {
    debugger
    this.checkedIDs = []
    this.getcourseforRegister.forEach((value, index) => {
      if (value.check) {
        this.checkedIDs.push(value.id);
      }
    });
  }
  AddCourse() {
    debugger
    this._studentService
      .registerCourses(this.rollno,this.checkedIDs)
      .subscribe(() => {
        abp.notify.success('Courses Added Successfully ');
      })
  }
}
