Drop table Replies
Drop table Discussion
Drop table Forum
Drop table Attendance
Drop table GroupList
Drop table Timetable_Course	
Drop table Assignment
Drop table Exam_Timetable
Drop table Exam_Result_Per_Course
Drop table Exam_Result
Drop table Student_Course_Register
Drop table Programme_In_Charge
Drop table Programme
Drop table Receipt
Drop table Payment
Drop table Course
Drop table FAQ
Drop table Message
Drop table Chat
Drop table Staff
Drop table Student
Drop table Branches
Drop table Faculty

CREATE TABLE [Faculty] (
	[FacultyID] char (6) NOT NULL,
	[FacultyName] varchar (100) NOT NULL,
	[FacultyDesc] varchar (399) NOT NULL,
	CONSTRAINT pk_faculty PRIMARY KEY (FacultyID)
) 

CREATE TABLE [Branches] (
	[BranchesID] char (6) NOT NULL,
	[BranchesName] varchar (50) NOT NULL,
	[State] varchar (50) NOT NULL,
	CONSTRAINT pk_branches PRIMARY KEY (BranchesID)
)

CREATE TABLE [Student] (
	[StudGUID] UNIQUEIDENTIFIER NOT NULL,
	[StudUsername] varchar(30) NOT NULL,
	[StudPassword] varchar(30) NOT NULL,
	[StudFullName] varchar(50) NOT NULL,
	[Gender] varchar (10) NOT NULL,
	[DOB] date NOT NULL,
	[PhoneNumber] varchar(15) NOT NULL,
	[Email] varchar(100) NOT NULL,
	[NRIC] varchar(15) NOT NULL,
	[YearIntake] varchar (15) NOT NULL,
	[AccountRegisterDate] datetime NOT NULL,
	[BranchesID] char (6)  NOT NULL,
	[FacultyID] char (6) NOT NULL,
	CONSTRAINT pk_student PRIMARY KEY (StudGUID),
	CONSTRAINT fk_stundent_branches FOREIGN KEY (BranchesID) REFERENCES Branches(BranchesID),
	CONSTRAINT fk_stundent_faculty FOREIGN KEY (FacultyID) REFERENCES Faculty(FacultyID) 
)

CREATE TABLE [Staff] (
	[StaffGUID] UNIQUEIDENTIFIER NOT NULL,
	[StaffUsername] varchar(30) NOT NULL,
	[StaffPassword] varchar(30) NOT NULL,
	[StaffFullName] varchar(50) NOT NULL,
	[StaffRole] varchar(30) NOT NULL,
	[PhoneNumber] varchar(15) NOT NULL,
	[Email] varchar(100) NOT NULL,
	[NRIC] varchar(15) NOT NULL,
	[Specialization] varchar (399) NOT NULL,
	[BranchesID] char (6) NOT NULL,
	[FacultyID] char (6) NOT NULL
	CONSTRAINT pk_staff PRIMARY KEY (StaffGUID),
	CONSTRAINT fk_staff_branches FOREIGN KEY (BranchesID) REFERENCES Branches(BranchesID),
	CONSTRAINT fk_staff_faculty FOREIGN KEY (FacultyID) REFERENCES Faculty(FacultyID) 
)

CREATE TABLE [Course] (
	[CourseID] char(10) NOT NULL,
	[CourseName] varchar (45) NOT NULL,
	[CourseDesc] varchar (80) NOT NULL,
	[CreditHour] int NOT NULL,
	[Category]   varchar (30) NOT NULL,
	[CourseFee]  decimal (10, 2) NOT NULL,
	CONSTRAINT pk_course PRIMARY KEY (CourseID),
)

CREATE TABLE [Programme] (
	[ProgrammeID] char(5) NOT NULL,
	[ProgrammeName] varchar(100) NOT NULL,
	[ProgrammeDesc] varchar(999) NOT NULL,
	[ProgrammeDuration] varchar (30) NOT NULL,
	CONSTRAINT pk_programme PRIMARY KEY (ProgrammeID),
)

CREATE TABLE [Payment] (
	[PaymentGUID] UNIQUEIDENTIFIER NOT NULL,
	[PaymentNo] char(15) NOT NULL,	
	[StudGUID] UNIQUEIDENTIFIER NOT NULL,
	[PaymentMethod] varchar(30) NOT NULL,
	[PaymentStatus] varchar(10) NOT NULL,
	[TotalAmount] decimal(10,2) NOT NULL,
	[DateIssued] datetime NOT NULL,
	[DateOverdue] datetime NOT NULL,
	CONSTRAINT pk_payment PRIMARY KEY (PaymentGUID),
	CONSTRAINT fk_student_payment FOREIGN KEY (StudGUID) REFERENCES Student(StudGUID)
)

CREATE TABLE [Receipt] (
	[ReceiptGUID] UNIQUEIDENTIFIER NOT NULL,
	[ReceiptNo] char(15) NOT NULL,
	[PaymentGUID] UNIQUEIDENTIFIER NOT NULL,
	[TotalAmount] decimal (10,2) NOT NULL,
	[DateIssued] datetime NOT NULL,
	CONSTRAINT pk_receipt PRIMARY KEY (ReceiptGUID),
	CONSTRAINT fk_payment_receipt FOREIGN KEY (PaymentGUID) REFERENCES Payment(PaymentGUID)
)

CREATE TABLE [Programme_In_Charge] (
	[ProgInChargeGUID] UNIQUEIDENTIFIER NOT NULL,
	[ProgrammeID] char(5) NOT NULL,
	[StaffGUID] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT pk_pic PRIMARY KEY (ProgInChargeGUID),
	CONSTRAINT fk_programme_pic FOREIGN KEY (ProgrammeID) REFERENCES Programme(ProgrammeID),
	CONSTRAINT fk_staff_pic FOREIGN KEY (StaffGUID) REFERENCES Staff(StaffGUID)
)

CREATE TABLE [Student_Course_Register] (
	[RegisterGUID] UNIQUEIDENTIFIER NOT NULL,
	[StudGUID] UNIQUEIDENTIFIER NOT NULL,
	[CourseID] char(10) NOT NULL,
	[ProgrammeID] char(5) NOT NULL,
	[CourseRegisterDate] datetime NOT NULL,
  	CONSTRAINT pk_register PRIMARY KEY (RegisterGUID),
	CONSTRAINT fk_student_scr FOREIGN KEY (StudGUID) REFERENCES Student(StudGUID),
	CONSTRAINT fk_course_scr FOREIGN KEY (CourseID) REFERENCES Course(CourseID),
	CONSTRAINT fk_programme_scr FOREIGN KEY (ProgrammeID) REFERENCES Programme(ProgrammeID)
)

CREATE TABLE [Exam_Result] (
	[Exam_ResultGUID] UNIQUEIDENTIFIER NOT NULL,
	[StudGUID] UNIQUEIDENTIFIER NOT NULL,
	[Sessions] varchar(100) NOT NULL,
	[TotalCourseRegistered] int NOT NULL,
	[GPA] decimal(5,4) NOT NULL,
	[CGPA] decimal(5,4) NOT NULL,
    CONSTRAINT pk_exam_result PRIMARY KEY (Exam_ResultGUID),
	CONSTRAINT fk_student_exam_result FOREIGN KEY (StudGUID) REFERENCES Student(StudGUID)
)

CREATE TABLE [Exam_Result_Per_Course] (
	[Exam_Result_Per_CourseGUID] UNIQUEIDENTIFIER NOT NULL,
	[StudGUID] UNIQUEIDENTIFIER NOT NULL,
	[CourseID] char(10) NOT NULL,
	[Mark] decimal(3,2) NOT NULL,
	[Grade] varchar (3) NOT NULL,
    CONSTRAINT pk_exam_result_per_course PRIMARY KEY (Exam_Result_Per_CourseGUID),
	CONSTRAINT fk_student_exam_result_per_course FOREIGN KEY (StudGUID) REFERENCES Student(StudGUID),
	CONSTRAINT fk_course_exam_result_per_course FOREIGN KEY (CourseID) REFERENCES Course(CourseID)
)

CREATE TABLE [Exam_Timetable] (
	[Exam_TimetableGUID] UNIQUEIDENTIFIER NOT NULL,
	[ExamDate] datetime NOT NULL,
	[CourseID] char(10) NOT NULL,
    CONSTRAINT pk_exam_timetable PRIMARY KEY (Exam_TimetableGUID),
	CONSTRAINT fk_course_exam_timetable FOREIGN KEY (CourseID) REFERENCES Course(CourseID)
)

CREATE TABLE [Forum] (
	[ForumGUID] UNIQUEIDENTIFIER NOT NULL,
	[StaffGUID] UNIQUEIDENTIFIER NOT NULL,
	[ForumTopic] varchar(100) NOT NULL,
	[ForumDesc] varchar(999) NOT NULL,
	[ForumCategory] varchar(100) NOT NULL,
	[Create_Date] datetime NOT NULL,
    CONSTRAINT pk_forum PRIMARY KEY (ForumGUID),
	CONSTRAINT fk_staff_forum FOREIGN KEY (StaffGUID) REFERENCES Staff(StaffGUID)
)

CREATE TABLE [Discussion] (
	[DiscGUID] UNIQUEIDENTIFIER NOT NULL,
	[ForumGUID] UNIQUEIDENTIFIER NOT NULL,
	[DiscTopic] varchar(100) NOT NULL,
	[DiscDesc] varchar(100) NOT NULL,
	[Create_Date] datetime NOT NULL,
	[Create_By] varchar (50) NOT NULL,
    CONSTRAINT pk_disc PRIMARY KEY (DiscGUID),
	CONSTRAINT fk_forum_disc FOREIGN KEY (ForumGUID) REFERENCES Forum(ForumGUID)

)

CREATE TABLE [Replies] (
	[ReplyGUID] UNIQUEIDENTIFIER NOT NULL,
	[DiscGUID] UNIQUEIDENTIFIER NOT NULL,
	[Reply_Content] varchar(999) NOT NULL,
	[Reply_Date] datetime NOT NULL, 
	[Reply_By] varchar (50) NOT NULL,
    CONSTRAINT pk_reply PRIMARY KEY (ReplyGUID),
	CONSTRAINT fk_disc_reply FOREIGN KEY (DiscGUID) REFERENCES Discussion(DiscGUID)
)

CREATE TABLE [FAQ] (
    [FAQGUID]         UNIQUEIDENTIFIER NOT NULL,
    [FAQTitle]        varchar (50)    NOT NULL,
    [FAQDescription]  varchar (300)   NOT NULL,
    [FAQCategory]     varchar (50)    NOT NULL,
    [FAQStatus]       varchar (20)    NOT NULL,
    [CreatedBy]       varchar (50)    NOT NULL,
    [CreatedDate]     DATETIME         NOT NULL,
    [LastUpdatedBy]   varchar (50)    NOT NULL,
    [LastUpdatedDate] DATETIME         NOT NULL,
    CONSTRAINT pk_faq PRIMARY KEY (FAQGUID)
);

CREATE TABLE [Chat] (
    [ChatGUID]    UNIQUEIDENTIFIER NOT NULL,
    [StudGUID] UNIQUEIDENTIFIER NOT NULL,
    [StaffGUID]   UNIQUEIDENTIFIER NOT NULL,
    [ChatStatus]  varchar (30)    NOT NULL,
    [CreatedDate] DATETIME         NOT NULL,
    [EndDate]     DATETIME         NULL,
    CONSTRAINT pk_chat PRIMARY KEY (ChatGUID),
	CONSTRAINT fk_student_chat FOREIGN KEY (StudGUID) REFERENCES Student(StudGUID),
	CONSTRAINT fk_staff_chat FOREIGN KEY (StaffGUID) REFERENCES Staff(StaffGUID)	
);

CREATE TABLE [Message] (
    [MessageGUID]   UNIQUEIDENTIFIER NOT NULL,
    [ChatGUID]      UNIQUEIDENTIFIER NOT NULL,
    [MessageDetail] varchar (300)   NOT NULL,
    [MessageType]   varchar (30)    NOT NULL,
    [UserType]      varchar (30)    NOT NULL,
    [SendDate]      DATETIME         NOT NULL,
    CONSTRAINT pk_message PRIMARY KEY (MessageGUID),
	CONSTRAINT fk_chat_message FOREIGN KEY (ChatGUID) REFERENCES Chat(ChatGUID)
);

CREATE TABLE [Assignment] (
	[AssignmentGUID] UNIQUEIDENTIFIER NOT NULL,
	[StudGUID] UNIQUEIDENTIFIER NOT NULL,
	[CourseID] char (10)  NOT NULL,
	[SubmissionDate] datetime NOT NULL,
	[SubmissionFile] varchar (300) NOT NULL,
	[FileType] varchar (30) NOT NULL,
    CONSTRAINT pk_assignment PRIMARY KEY (AssignmentGUID),
	CONSTRAINT fk_student_assignment FOREIGN KEY (StudGUID) REFERENCES Student(StudGUID),
	CONSTRAINT fk_course_assignment FOREIGN KEY (CourseID) REFERENCES Course(CourseID),
);

CREATE TABLE [Timetable_Course] (
	[Timetable_CourseGUID] UNIQUEIDENTIFIER NOT NULL,
	[CourseID] char(10) NOT NULL,
	[Start_Time] datetime NOT NULL,
	[End_Time] datetime NOT NULL,
	[Days_of_week] varchar(10) NOT NULL,
	[Class_Type] varchar(20) NOT NULL,
	[Class_Category] varchar(20) NOT NULL,
    CONSTRAINT pk_timetable_course PRIMARY KEY (Timetable_CourseGUID),
	CONSTRAINT fk_course_timetable_course FOREIGN KEY (CourseID) REFERENCES Course(CourseID),
);

CREATE TABLE [GroupList] (
	[GroupListGUID] UNIQUEIDENTIFIER NOT NULL,
	[CourseID] char(10) NOT NULL,
	[StaffGUID] UNIQUEIDENTIFIER NOT NULL,
	[Timetable_CourseGUID] UNIQUEIDENTIFIER NOT NULL,
	[GroupNo] int NOT NULL,
    CONSTRAINT pk_group_list PRIMARY KEY (GroupListGUID),
	CONSTRAINT fk_course_group_list FOREIGN KEY (CourseID) REFERENCES Course(CourseID),
	CONSTRAINT fk_staff_group_list FOREIGN KEY (StaffGUID) REFERENCES Staff(StaffGUID),
	CONSTRAINT fk_timetable_course_group_list FOREIGN KEY (Timetable_CourseGUID) REFERENCES Timetable_Course(Timetable_CourseGUID),
);

CREATE TABLE [Attendance] (
	[AttendanceGUID] UNIQUEIDENTIFIER NOT NULL,
	[CourseID] char(10) NOT NULL,
	[GroupListGUID] UNIQUEIDENTIFIER NOT NULL,
	[StudGUID] UNIQUEIDENTIFIER NOT NULL,
	[AttendanceStatus] varchar(10) NOT NULL,
    CONSTRAINT pk_attendance PRIMARY KEY (AttendanceGUID),
	CONSTRAINT fk_course_attendance FOREIGN KEY (CourseID) REFERENCES Course(CourseID),
	CONSTRAINT fk_grouplistcourse_attendance FOREIGN KEY (GroupListGUID) REFERENCES GroupList(GroupListGUID),
	CONSTRAINT fk_student_attendance FOREIGN KEY (StudGUID) REFERENCES Student(StudGUID),
);


CREATE TABLE [Semester] (
	[SemesterGUID] UNIQUEIDENTIFIER NOT NULL,
	[SemesterName] varchar(30) NOT NULL,
	[SemesterStartDate] Date NOT NULL,
	[SemesterEndDate] Date NOT NULL,
	[SemesterWeekDuration] int NOT NULL,
	[SemesterStudyDayDuration] int NOT NULL,
	[SemesterExamDayDuration] int NOT NULL,
	[SemesterBreakDayDuration] int NOT NULL,
	CONSTRAINT pk_attendance PRIMARY KEY (SemesterGUID)
);




