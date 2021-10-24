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

CREATE TABLE [dbo].[Faculty] (
    [FacultyGUID]  UNIQUEIDENTIFIER NOT NULL,
    [FacultyAbbrv] VARCHAR (5)      NOT NULL,
    [FacultyName]  VARCHAR (100)    NOT NULL,
    [FacultyDesc]  VARCHAR (399)    NOT NULL,
    CONSTRAINT [pk_faculty] PRIMARY KEY CLUSTERED ([FacultyGUID] ASC)
);

CREATE TABLE [Branches] (
	[BranchesGUID] UNIQUEIDENTIFIER NOT NULL,
	[BranchesName] varchar (50) NOT NULL,
	[BranchesAddress] varchar (200) NOT NULL,
	[BranchesEmail] varchar(100) NOT NULL,
	[BranchesTel] varchar(100) NOT NULL,
	CONSTRAINT pk_branches PRIMARY KEY (BranchesGUID)
)

CREATE TABLE [dbo].[Student] (
	[StudentGUID]            UNIQUEIDENTIFIER NOT NULL,
	[StudentUsername]        VARCHAR (30)     NOT NULL,
	[StudentPassword]        VARCHAR (50)     NOT NULL,
	[StudentFullName]        VARCHAR (50)     NOT NULL,
    [Gender]              VARCHAR (10)     NOT NULL,
    [DOB]                 DATE             NOT NULL,
    [PhoneNumber]         VARCHAR (15)     NOT NULL,
    [Email]               VARCHAR (100)    NOT NULL,
    [NRIC]                VARCHAR (15)     NOT NULL,
	[Address]			  VARCHAR(150)	   NOT NULL,
	[ProfileImage]		  VARCHAR(500)     NULL,
	[LastUpdateInfo]      DATETIME		   NOT NULL,
    [YearIntake]          VARCHAR (15)     NOT NULL,
    [AccountRegisterDate] DATETIME         NOT NULL,
	[LastUpdateInfo]      DATETIME         NULL,
    [BranchesGUID]        UNIQUEIDENTIFIER         NOT NULL,
    [FacultyGUID]         UNIQUEIDENTIFIER         NOT NULL,
    CONSTRAINT [pk_student] PRIMARY KEY CLUSTERED ([StudentGUID] ASC),
    CONSTRAINT [fk_stundent_branches] FOREIGN KEY ([BranchesGUID]) REFERENCES [dbo].[Branches] ([BranchesGUID]),
    CONSTRAINT [fk_stundent_faculty] FOREIGN KEY ([FacultyGUID]) REFERENCES [dbo].[Faculty] ([FacultyGUID])
);

CREATE TABLE [dbo].[Staff] (
    [StaffGUID]      UNIQUEIDENTIFIER NOT NULL,
    [StaffUsername]  VARCHAR (30)     NOT NULL,
    [StaffPassword]  VARCHAR (50)     NOT NULL,
    [StaffFullName]  VARCHAR (50)     NOT NULL,
    [Gender]         VARCHAR (10)     NOT NULL,
    [StaffRole]      VARCHAR (50)     NOT NULL,
	[StaffPositiion] VARCHAR (100)    NOT NULL,
    [StaffStatus]    VARCHAR (10)     NOT NULL,
    [PhoneNumber]    VARCHAR (15)     NOT NULL,
    [Email]          VARCHAR (100)    NOT NULL,
    [NRIC]           VARCHAR (15)     NOT NULL,
	[ProfileImage]   VARCHAR (500)    NULL,
	[LastUpdateInfo] DATETIME         NULL,
    [Specialization] VARCHAR (399)    NOT NULL,
    [BranchesGUID]   UNIQUEIDENTIFIER NOT NULL,
    [FacultyGUID]    UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [pk_staff] PRIMARY KEY CLUSTERED ([StaffGUID] ASC),
    CONSTRAINT [fk_staff_branches] FOREIGN KEY ([BranchesGUID]) REFERENCES [dbo].[Branches] ([BranchesGUID]),
    CONSTRAINT [fk_staff_faculty] FOREIGN KEY ([FacultyGUID]) REFERENCES [dbo].[Faculty] ([FacultyGUID])
);

CREATE TABLE [Course] (
	[CourseGUID] UNIQUEIDENTIFIER NOT NULL,
	[CourseAbbrv] varchar (9) NOT NULL,
	[CourseName] varchar (45) NOT NULL,
	[CourseDesc] varchar (80) NOT NULL,
	[CreditHour] int NOT NULL,
	[Category]   varchar (30) NOT NULL,
	CONSTRAINT pk_course PRIMARY KEY (CourseGUID),
)

CREATE TABLE [dbo].[Programme] (
    [ProgrammeGUID]       UNIQUEIDENTIFIER NOT NULL,
    [ProgrammeAbbrv]      VARCHAR (4)      NOT NULL,
    [ProgrammeName]       VARCHAR (100)    NOT NULL,
    [ProgrammeDesc]       VARCHAR (999)    NOT NULL,
    [ProgrammeDuration]   VARCHAR (30)     NOT NULL,
    [ProgrammeCategory]   VARCHAR (30)     NOT NULL,
    [ProgrammeFullorPart] VARCHAR (10)     NULL,
	[ProgrammeFaculty]    VARCHAR (100)    NOT NULL,
    CONSTRAINT [pk_programme] PRIMARY KEY CLUSTERED ([ProgrammeGUID] ASC)
);


CREATE TABLE [Payment] (
	[PaymentGUID] UNIQUEIDENTIFIER NOT NULL,
	[PaymentNo] char(15) NOT NULL,	
	[StudentGUID] UNIQUEIDENTIFIER NOT NULL,
	[PaymentMethod] varchar(30) NOT NULL,
	[PaymentStatus] varchar(10) NOT NULL,
	[TotalAmount] decimal(10,2) NOT NULL,
	[DateIssued] datetime NOT NULL,
	[DateOverdue] datetime NOT NULL,
	CONSTRAINT pk_payment PRIMARY KEY (PaymentGUID),
	CONSTRAINT fk_student_payment FOREIGN KEY (StudentGUID) REFERENCES Student(StudentGUID)
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
	CONSTRAINT fk_programme_pic FOREIGN KEY (ProgrammeGUID) REFERENCES Programme(ProgrammeGUID),
	CONSTRAINT fk_staff_pic FOREIGN KEY (StaffGUID) REFERENCES Staff(StaffGUID)
)

CREATE TABLE [Student_Course_Register] (
	[RegisterGUID] UNIQUEIDENTIFIER NOT NULL,
	[StudentGUID] UNIQUEIDENTIFIER NOT NULL,
	[CourseID] char(10) NOT NULL,
	[ProgrammeID] char(5) NOT NULL,
	[CourseRegisterDate] datetime NOT NULL,
  	CONSTRAINT pk_register PRIMARY KEY (RegisterGUID),
	CONSTRAINT fk_student_scr FOREIGN KEY (StudentGUID) REFERENCES Student(StudentGUID),
	CONSTRAINT fk_course_scr FOREIGN KEY (CourseGUID) REFERENCES Course(CourseGUID),
	CONSTRAINT fk_programme_scr FOREIGN KEY (ProgrammeGUID) REFERENCES Programme(ProgrammeGUID)
)

CREATE TABLE [Exam_Result] (
	[Exam_ResultGUID] UNIQUEIDENTIFIER NOT NULL,
	[StudentGUID] UNIQUEIDENTIFIER NOT NULL,
	[Sessions] varchar(100) NOT NULL,
	[TotalCourseRegistered] int NOT NULL,
	[GPA] decimal(5,4) NOT NULL,
	[CGPA] decimal(5,4) NOT NULL,
    CONSTRAINT pk_exam_result PRIMARY KEY (Exam_ResultGUID),
	CONSTRAINT fk_student_exam_result FOREIGN KEY (StudentGUID) REFERENCES Student(StudentGUID)
)

CREATE TABLE [Exam_Result_Per_Course] (
	[Exam_Result_Per_CourseGUID] UNIQUEIDENTIFIER NOT NULL,
	[StudentGUID] UNIQUEIDENTIFIER NOT NULL,
	[CourseID] char(10) NOT NULL,
	[Mark] decimal(3,2) NOT NULL,
	[Grade] varchar (3) NOT NULL,
    CONSTRAINT pk_exam_result_per_course PRIMARY KEY (Exam_Result_Per_CourseGUID),
	CONSTRAINT fk_student_exam_result_per_course FOREIGN KEY (StudentGUID) REFERENCES Student(StudentGUID),
	CONSTRAINT fk_course_exam_result_per_course FOREIGN KEY (CourseGUID) REFERENCES Course(CourseGUID)
)

CREATE TABLE [Exam_Timetable] (
	[Exam_TimetableGUID] UNIQUEIDENTIFIER NOT NULL,
	[ExamDate] datetime NOT NULL,
	[CourseID] char(10) NOT NULL,
    CONSTRAINT pk_exam_timetable PRIMARY KEY (Exam_TimetableGUID),
	CONSTRAINT fk_course_exam_timetable FOREIGN KEY (CourseGUID) REFERENCES Course(CourseGUID)
)

CREATE TABLE [dbo].[Forum] (
    [ForumGUID]            UNIQUEIDENTIFIER NOT NULL,
    [StaffGUID]            UNIQUEIDENTIFIER NOT NULL,
    [ForumTopic]           VARCHAR (100)    NOT NULL,
    [ForumDesc]            VARCHAR (999)    NOT NULL,
    [ForumCategory]        VARCHAR (100)    NOT NULL,
    [ForumStatus]          VARCHAR (10)     NOT NULL,
	[ForumType]			   VARCHAR (10)		NOT NULL,
	[ForumCreatedBy]       VARCHAR (50)     NOT NULL,
    [ForumCreatedDate]     DATETIME         NOT NULL,
    [ForumLastUpdatedBy]   VARCHAR (50)     NOT NULL,
    [ForumLastUpdatedDate] DATETIME         NOT NULL,
    CONSTRAINT [pk_forum] PRIMARY KEY CLUSTERED ([ForumGUID] ASC),
    CONSTRAINT [fk_staff_forum] FOREIGN KEY ([StaffGUID]) REFERENCES [dbo].[Staff] ([StaffGUID])
);

CREATE TABLE [dbo].[Discussion] (
    [DiscGUID]            UNIQUEIDENTIFIER NOT NULL,
    [ForumGUID]           UNIQUEIDENTIFIER NOT NULL,
    [DiscTopic]           VARCHAR (100)    NOT NULL,
    [DiscDesc]            VARCHAR (100)    NOT NULL,
	[DiscContent]         VARCHAR (999)    NOT NULL,
    [DiscStatus]          VARCHAR (10)     NOT NULL,
	[DiscIsPinned]		  VARCHAR (4)	   NOT NULL,
	[DiscIsLocked]        VARCHAR (4)      NOT NULL,
    [DiscCreatedBy]       VARCHAR (50)     NOT NULL,
    [DiscCreatedDate]     DATETIME         NOT NULL,
    [DiscLastUpdatedBy]   VARCHAR (50)     NOT NULL,
    [DiscLastUpdatedDate] DATETIME         NOT NULL,
    CONSTRAINT [pk_disc] PRIMARY KEY CLUSTERED ([DiscGUID] ASC),
    CONSTRAINT [fk_forum_disc] FOREIGN KEY ([ForumGUID]) REFERENCES [dbo].[Forum] ([ForumGUID])
);

CREATE TABLE [dbo].[Replies] (
    [ReplyGUID]     UNIQUEIDENTIFIER NOT NULL,
    [DiscGUID]      UNIQUEIDENTIFIER NOT NULL,
    [ReplyContent] VARCHAR (999)    NOT NULL,
    [ReplyDate]    DATETIME         NOT NULL,
    [ReplyBy]      VARCHAR (50)     NOT NULL,
    CONSTRAINT [pk_reply] PRIMARY KEY CLUSTERED ([ReplyGUID] ASC),
    CONSTRAINT [fk_disc_reply] FOREIGN KEY ([DiscGUID]) REFERENCES [dbo].[Discussion] ([DiscGUID])
);

CREATE TABLE [dbo].[ForumReport] (
    [ForumReportGUID] UNIQUEIDENTIFIER NOT NULL,
    [DiscGUID]        UNIQUEIDENTIFIER NULL,
    [ReplyGUID]       UNIQUEIDENTIFIER NULL,
    [ReportReason]    VARCHAR(999) NOT NULL,
	[ReportStatus]    VARCHAR (20) NOT NULL,
    [ReportBy]        VARCHAR (50)     NOT NULL,
    [ReportDate]      DATETIME         NOT NULL,
    CONSTRAINT [pk_forumreport] PRIMARY KEY CLUSTERED ([ForumReportGUID] ASC),
);

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
    [StudentGUID] UNIQUEIDENTIFIER NOT NULL,
    [StaffGUID]   UNIQUEIDENTIFIER NOT NULL,
    [ChatStatus]  varchar (30)    NOT NULL,
    [CreatedDate] DATETIME         NOT NULL,
    [EndDate]     DATETIME         NULL,
    CONSTRAINT pk_chat PRIMARY KEY (ChatGUID),
	CONSTRAINT fk_student_chat FOREIGN KEY (StudentGUID) REFERENCES Student(StudentGUID),
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
	[StudentGUID] UNIQUEIDENTIFIER NOT NULL,
	[CourseID] char (10)  NOT NULL,
	[SubmissionDate] datetime NOT NULL,
	[SubmissionFile] varchar (300) NOT NULL,
	[FileType] varchar (30) NOT NULL,
    CONSTRAINT pk_assignment PRIMARY KEY (AssignmentGUID),
	CONSTRAINT fk_student_assignment FOREIGN KEY (StudentGUID) REFERENCES Student(StudentGUID),
	CONSTRAINT fk_course_assignment FOREIGN KEY (CourseGUID) REFERENCES Course(CourseGUID),
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
	CONSTRAINT fk_course_timetable_course FOREIGN KEY (CourseGUID) REFERENCES Course(CourseGUID),
);

CREATE TABLE [GroupList] (
	[GroupListGUID] UNIQUEIDENTIFIER NOT NULL,
	[CourseID] char(10) NOT NULL,
	[StaffGUID] UNIQUEIDENTIFIER NOT NULL,
	[Timetable_CourseGUID] UNIQUEIDENTIFIER NOT NULL,
	[GroupNo] int NOT NULL,
    CONSTRAINT pk_group_list PRIMARY KEY (GroupListGUID),
	CONSTRAINT fk_course_group_list FOREIGN KEY (CourseGUID) REFERENCES Course(CourseGUID),
	CONSTRAINT fk_staff_group_list FOREIGN KEY (StaffGUID) REFERENCES Staff(StaffGUID),
	CONSTRAINT fk_timetable_course_group_list FOREIGN KEY (Timetable_CourseGUID) REFERENCES Timetable_Course(Timetable_CourseGUID),
);

CREATE TABLE [Attendance] (
	[AttendanceGUID] UNIQUEIDENTIFIER NOT NULL,
	[CourseID] char(10) NOT NULL,
	[GroupListGUID] UNIQUEIDENTIFIER NOT NULL,
	[StudentGUID] UNIQUEIDENTIFIER NOT NULL,
	[AttendanceStatus] varchar(10) NOT NULL,
    CONSTRAINT pk_attendance PRIMARY KEY (AttendanceGUID),
	CONSTRAINT fk_course_attendance FOREIGN KEY (CourseGUID) REFERENCES Course(CourseGUID),
	CONSTRAINT fk_grouplistcourse_attendance FOREIGN KEY (GroupListGUID) REFERENCES GroupList(GroupListGUID),
	CONSTRAINT fk_student_attendance FOREIGN KEY (StudentGUID) REFERENCES Student(StudentGUID),
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

CREATE TABLE [dbo].[ResetPassword] (
    [ResetPasswordGUID]            UNIQUEIDENTIFIER NOT NULL,
    [StudentGUID]        UNIQUEIDENTIFIER NULL,
    [StaffGUID]        UNIQUEIDENTIFIER NULL,
    [Status]        VARCHAR (20)     NOT NULL,
    [LinkToken]              VARCHAR (6)     NOT NULL,
    [CreatedTime]                 DATETIME             NOT NULL,
    [ExpiredTime]         DATETIME    NOT NULL,
	PRIMARY KEY CLUSTERED ([ResetPasswordGUID] ASC),
    CONSTRAINT [fk_stundent_resetpassowrd] FOREIGN KEY ([StudentGUID]) REFERENCES [dbo].[Student] ([StudentGUID]),
    CONSTRAINT [fk_staff_resetpassowrd] FOREIGN KEY ([StaffGUID]) REFERENCES [dbo].[Staff] ([StaffGUID])
);

CREATE TABLE [dbo].[Notification] (
    [NotificationGUID]         UNIQUEIDENTIFIER    NOT NULL,
    [CitizenGUID]               UNIQUEIDENTIFIER     NOT NULL,
    [NotificationSubject]   VARCHAR (50)        NOT NULL,
    [NotificationContent]     VARCHAR (500)         NOT NULL,
    [ReadFlag]                CHAR (1)            NOT NULL,
    [SentDate]                   DATETIME            NOT NULL,
    [SentBy]                  VARCHAR (50)            NULL,
    [NotificationDesciption]     VARCHAR (300)           NULL,
    PRIMARY KEY CLUSTERED ([NotificationGUID] ASC)
);