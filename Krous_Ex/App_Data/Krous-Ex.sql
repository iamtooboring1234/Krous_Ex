Drop table Replies
Drop table Discussion
Drop table Forum
Drop table Attendance
Drop table GroupList
Drop table Timetable_Course	
Drop table Assignment
Drop table Exam_Timetable
Drop table ExamResultPerCourse
Drop table ExamResult
Drop table Student_Programme_Register
Drop table Course_In_Charge
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

CREATE TABLE [dbo].[Branches] (
    [BranchesGUID]    UNIQUEIDENTIFIER NOT NULL,
    [BranchesName]    VARCHAR (50)     NOT NULL,
    [BranchesAddress] VARCHAR (200)    NOT NULL,
    [BranchesEmail]   VARCHAR (100)    NOT NULL,
    [BranchesTel]     VARCHAR (100)    NOT NULL,
    CONSTRAINT [pk_branches] PRIMARY KEY CLUSTERED ([BranchesGUID] ASC)
);

CREATE TABLE [dbo].[Student] (
    [StudentGUID]         UNIQUEIDENTIFIER NOT NULL,
    [StudentUsername]     VARCHAR (30)     NOT NULL,
    [StudentPassword]     VARCHAR (50)     NOT NULL,
    [StudentFullName]     VARCHAR (50)     NOT NULL,
    [Gender]              VARCHAR (10)     NOT NULL,
    [DOB]                 DATE             NOT NULL,
    [PhoneNumber]         VARCHAR (15)     NOT NULL,
    [Email]               VARCHAR (100)    NOT NULL,
    [NRIC]                VARCHAR (15)     NOT NULL,
    [Address]             VARCHAR (150)    NOT NULL,
    [ProfileImage]        VARCHAR (500)    NULL,
    [AccountRegisterDate] DATETIME         NOT NULL,
    [LastUpdateInfo]      DATETIME         NULL,
    [StudyStatus]         VARCHAR (20)     NULL,
    [BranchesGUID]        UNIQUEIDENTIFIER NULL,
    [FacultyGUID]         UNIQUEIDENTIFIER NULL,
    [SessionGUID]         UNIQUEIDENTIFIER NULL,
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

CREATE TABLE [dbo].[Course] (
    [CourseGUID]  UNIQUEIDENTIFIER NOT NULL,
    [CourseAbbrv] VARCHAR (9)      NOT NULL,
    [CourseName]  VARCHAR (45)     NOT NULL,
    [CourseDesc]  VARCHAR (80)     NOT NULL,
    [CreditHour]  INT              NOT NULL,
    [Category]    VARCHAR (30)     NOT NULL,
    CONSTRAINT [pk_course] PRIMARY KEY CLUSTERED ([CourseGUID] ASC)
);

CREATE TABLE [dbo].[Programme] (
    [ProgrammeGUID]       UNIQUEIDENTIFIER NOT NULL,
    [ProgrammeAbbrv]      VARCHAR (4)      NOT NULL,
    [ProgrammeName]       VARCHAR (100)    NOT NULL,
    [ProgrammeDesc]       VARCHAR (999)    NOT NULL,
    [ProgrammeDuration]   VARCHAR (30)     NOT NULL,
    [ProgrammeCategory]   VARCHAR (30)     NOT NULL,
    [ProgrammeFullorPart] VARCHAR (10)     NULL,
	[FacultyGUID]         UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [pk_programme] PRIMARY KEY CLUSTERED ([ProgrammeGUID] ASC),
    CONSTRAINT fk_faulty_prog FOREIGN KEY (FacultyGUID) REFERENCES Faculty(FacultyGUID)
);

CREATE TABLE [dbo].[Payment] (
    [PaymentGUID]   UNIQUEIDENTIFIER NOT NULL,
    [PaymentNo]     CHAR (15)        NOT NULL,
    [StudentGUID]   UNIQUEIDENTIFIER NOT NULL,
    [PaymentMethod] VARCHAR (30)     NULL,
    [PaymentStatus] VARCHAR (10)     NOT NULL,
    [PaymentDate]   DATETIME         NULL,
    [TotalAmount]   DECIMAL (10, 2)  NOT NULL,
	[TotalPaid]		DECIMAL (10, 2)  NULL,
    [DateIssued]    DATETIME         NOT NULL,
    [DateOverdue]   DATETIME         NOT NULL,
    CONSTRAINT [pk_payment] PRIMARY KEY CLUSTERED ([PaymentGUID] ASC),
    CONSTRAINT [fk_student_payment] FOREIGN KEY ([StudentGUID]) REFERENCES [dbo].[Student] ([StudentGUID])
);

CREATE TABLE [Receipt] (
	[ReceiptGUID] UNIQUEIDENTIFIER NOT NULL,
	[ReceiptNo] char(15) NOT NULL,
	[PaymentGUID] UNIQUEIDENTIFIER NOT NULL,
	[DateIssued] datetime NOT NULL,
	CONSTRAINT pk_receipt PRIMARY KEY (ReceiptGUID),
	CONSTRAINT fk_payment_receipt FOREIGN KEY (PaymentGUID) REFERENCES Payment(PaymentGUID)
)

CREATE TABLE [Course_In_Charge] (
	[CourseInChargeGUID] UNIQUEIDENTIFIER NOT NULL,
	[CourseGUID] UNIQUEIDENTIFIER NOT NULL,
	[StaffGUID] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT pk_cic PRIMARY KEY (CourseInChargeGUID),
	CONSTRAINT fk_course_cic FOREIGN KEY (CourseGUID) REFERENCES Course(CourseGUID),
	CONSTRAINT fk_staff_cic FOREIGN KEY (StaffGUID) REFERENCES Staff(StaffGUID)
)

CREATE TABLE [dbo].[Student_Programme_Register] (
    [RegisterGUID]          UNIQUEIDENTIFIER NOT NULL,
    [StudentGUID]           UNIQUEIDENTIFIER NOT NULL,
    [ProgrammeGUID]         UNIQUEIDENTIFIER NOT NULL,
    [SessionGUID]           UNIQUEIDENTIFIER NOT NULL,
	[SemesterGUID]			UNIQUEIDENTIFIER NOT NULL,
    [ProgrammeRegisterDate] DATETIME         NOT NULL,
    [Status]                VARCHAR (10)     NOT NULL,
    [UploadIcImage]         VARCHAR (500)    NOT NULL,
    [UploadResult]          VARCHAR (500)    NOT NULL,
    [UploadMedical]         VARCHAR (500)    NULL,
    CONSTRAINT [pk_register] PRIMARY KEY CLUSTERED ([RegisterGUID] ASC),
    CONSTRAINT [fk_student_scr] FOREIGN KEY ([StudentGUID]) REFERENCES [dbo].[Student] ([StudentGUID]),
    CONSTRAINT [fk_programme_scr] FOREIGN KEY ([ProgrammeGUID]) REFERENCES [dbo].[Programme] ([ProgrammeGUID]),
    CONSTRAINT [fk_session_scr] FOREIGN KEY ([SessionGUID]) REFERENCES [dbo].[Session] ([SessionGUID]),
	CONSTRAINT [fk_semester_scr] FOREIGN KEY ([SemesterGUID]) REFERENCES [dbo].[Semester] ([SemesterGUID])
);

CREATE TABLE [dbo].[ExamResult] (
    [ExamResultGUID] UNIQUEIDENTIFIER NOT NULL,
    [SessionGUID]    UNIQUEIDENTIFIER NOT NULL,
    [StudentGUID]    UNIQUEIDENTIFIER NOT NULL,
    [GPA]            DECIMAL (5, 4)   NULL,
    [CGPA]           DECIMAL (5, 4)   NULL,
    [Status]         VARCHAR (50)     NOT NULL,
	[ReleaseDate]	 DATETIME		  NULL,
    CONSTRAINT [pk_examResult] PRIMARY KEY CLUSTERED ([ExamResultGUID] ASC),
    CONSTRAINT [fk_session_examResult] FOREIGN KEY ([SessionGUID]) REFERENCES [dbo].[Session] ([SessionGUID]),
    CONSTRAINT [fk_student_examResult] FOREIGN KEY ([StudentGUID]) REFERENCES [dbo].[Student] ([StudentGUID])
);


CREATE TABLE [dbo].[ExamResultPerCourse] (
    [ExamResultPerCourseGUID] UNIQUEIDENTIFIER NOT NULL,
    [ExamResultGUID]          UNIQUEIDENTIFIER NOT NULL,
    [CourseGUID]              UNIQUEIDENTIFIER NOT NULL,
    [Mark]                    INT              NULL,
    [Grade]                   VARCHAR (3)      NULL,
    [Remarks]                 VARCHAR (100)    NULL,
    CONSTRAINT [pk_exam_result_per_course] PRIMARY KEY CLUSTERED ([ExamResultPerCourseGUID] ASC),
    CONSTRAINT [fk_exam_result_exam_result_per_course] FOREIGN KEY ([ExamResultGUID]) REFERENCES [dbo].[ExamResult] ([ExamResultGUID]),
    CONSTRAINT [fk_course_exam_result_per_course] FOREIGN KEY ([CourseGUID]) REFERENCES [dbo].[Course] ([CourseGUID])
);

CREATE TABLE [dbo].[ExamTimetable] (
    [ExamTimetableGUID] UNIQUEIDENTIFIER NOT NULL,
    [SessionGUID]       UNIQUEIDENTIFIER NOT NULL,
    [CourseGUID]        UNIQUEIDENTIFIER NOT NULL,
	[MeetingLinkGUID]	UNIQUEIDENTIFIER NULL,
    [ExamStartDateTime] DATETIME         NOT NULL,
    [ExamEndDateTime]   DATETIME         NOT NULL,
    CONSTRAINT [pk_exam_timetable] PRIMARY KEY CLUSTERED ([ExamTimetableGUID] ASC),
    CONSTRAINT [fk_course_examtimetable] FOREIGN KEY ([CourseGUID]) REFERENCES [dbo].[Course] ([CourseGUID]),
	CONSTRAINT fk_meetinglink_examtimetable FOREIGN KEY ([MeetingLinkGUID]) REFERENCES MeetingLink([MeetingLinkGUID])
);

CREATE TABLE [ExamPreparation] (
	[ExaminationPreparationGUID] UNIQUEIDENTIFIER NOT NULL,
    [ExamTimetableGUID] UNIQUEIDENTIFIER NOT NULL,
    [QuestionPaper] VARCHAR(500) NOT NULL,
    [AnswerSheet] VARCHAR(500) NOT NULL,
    CONSTRAINT pk_examPrep PRIMARY KEY ([ExaminationPreparationGUID] ASC),
	CONSTRAINT fk_examTimetable_examPrep FOREIGN KEY ([ExamTimetableGUID]) REFERENCES ExamTimetable([ExamTimetableGUID])
);

CREATE TABLE [dbo].[Forum] (
    [ForumGUID]            UNIQUEIDENTIFIER NOT NULL,
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

CREATE TABLE [dbo].[FAQ] (
    [FAQGUID]         UNIQUEIDENTIFIER NOT NULL,
    [FAQTitle]        VARCHAR (50)     NOT NULL,
    [FAQDescription]  VARCHAR (300)    NOT NULL,
    [FAQCategory]     VARCHAR (50)     NOT NULL,
    [FAQStatus]       VARCHAR (20)     NOT NULL,
    [CreatedBy]       VARCHAR (50)     NOT NULL,
    [CreatedDate]     DATETIME         NOT NULL,
    [LastUpdatedBy]   VARCHAR (50)     NOT NULL,
    [LastUpdatedDate] DATETIME         NOT NULL,
    CONSTRAINT [pk_faq] PRIMARY KEY CLUSTERED ([FAQGUID] ASC)
);

CREATE TABLE [dbo].[Chat] (
    [ChatGUID]    UNIQUEIDENTIFIER NOT NULL,
    [StudentGUID] UNIQUEIDENTIFIER NULL,
    [StaffGUID]   UNIQUEIDENTIFIER NULL,
    [ChatStatus]  VARCHAR (30)     NOT NULL,
    [CreatedDate] DATETIME         NOT NULL,
    [EndDate]     DATETIME         NULL,
    CONSTRAINT [pk_chat] PRIMARY KEY CLUSTERED ([ChatGUID] ASC),
    CONSTRAINT [fk_student_chat] FOREIGN KEY ([StudentGUID]) REFERENCES [dbo].[Student] ([StudentGUID]),
    CONSTRAINT [fk_staff_chat] FOREIGN KEY ([StaffGUID]) REFERENCES [dbo].[Staff] ([StaffGUID])
);

CREATE TABLE [dbo].[Message] (
    [MessageGUID]   UNIQUEIDENTIFIER NOT NULL,
    [ChatGUID]      UNIQUEIDENTIFIER NOT NULL,
    [MessageDetail] VARCHAR (300)    NOT NULL,
    [MessageType]   VARCHAR (30)     NOT NULL,
    [UserType]      VARCHAR (30)     NOT NULL,
    [SendDate]      DATETIME         NOT NULL,
    CONSTRAINT [pk_message] PRIMARY KEY CLUSTERED ([MessageGUID] ASC),
    CONSTRAINT [fk_chat_message] FOREIGN KEY ([ChatGUID]) REFERENCES [dbo].[Chat] ([ChatGUID])
);

CREATE TABLE [dbo].[Submission] (
    [SubmissionGUID]   UNIQUEIDENTIFIER NOT NULL,
    [StudentGUID]      UNIQUEIDENTIFIER NOT NULL,
    [AssessmentGUID]   UNIQUEIDENTIFIER NOT NULL,
    [SubmissionDate]   DATETIME         NOT NULL,
    [SubmissionFile]   VARCHAR (300)    NULL,
    [SubmissionStatus] VARCHAR (15)     NOT NULL,
    [FileType]         VARCHAR (30)     NOT NULL,
    CONSTRAINT [pk_submission] PRIMARY KEY CLUSTERED ([SubmissionGUID] ASC),
    CONSTRAINT [fk_student_submission] FOREIGN KEY ([StudentGUID]) REFERENCES [dbo].[Student] ([StudentGUID]),
    CONSTRAINT [fk_assessment_submission] FOREIGN KEY ([AssessmentGUID]) REFERENCES [dbo].[Assessment] ([AssessmentGUID])
);

CREATE TABLE [dbo].[ExamSubmission] (
    [ExamSubmissionGUID] UNIQUEIDENTIFIER NOT NULL,
    [StudentGUID]        UNIQUEIDENTIFIER NOT NULL,
    [ExamTimetableGUID]  UNIQUEIDENTIFIER NOT NULL,
    [SubmissionDate]     DATETIME         NOT NULL,
    [SubmissionFile]     VARCHAR (300)    NULL,
    [SubmissionStatus]   VARCHAR (15)     NOT NULL,
    CONSTRAINT [pk_examsubmission] PRIMARY KEY CLUSTERED ([ExamSubmissionGUID] ASC),
    CONSTRAINT [fk_student_examsubmission] FOREIGN KEY ([StudentGUID]) REFERENCES [dbo].[Student] ([StudentGUID]),
    CONSTRAINT [fk_examtimetable_examsubmission] FOREIGN KEY ([ExamTimetableGUID]) REFERENCES [dbo].[ExamTimetable] ([ExamTimetableGUID])
);

CREATE TABLE [TimetableCourse] (
	[TimetableCourseGUID] UNIQUEIDENTIFIER NOT NULL,
	[ProgrammeCourseGUID] UNIQUEIDENTIFIER NOT NULL,
    [SessionGUID] UNIQUEIDENTIFIER NOT NULL,
    [GroupGUID] UNIQUEIDENTIFIER NOT NULL,
    [StaffGUID] UNIQUEIDENTIFIER NOT NULL,
	[ClassStartTime] DATETIME NOT NULL,
	[ClassEndTime] DATETIME NOT NULL,
	[DaysOfWeek] VARCHAR(10) NOT NULL,
	[ClassType] VARCHAR(20) NOT NULL,
	[ClassCategory] VARCHAR(20) NOT NULL,
    CONSTRAINT pk_timetable_course PRIMARY KEY ([TimetableCourseGUID]),
	CONSTRAINT fk_course_timetable_course FOREIGN KEY ([ProgrammeCourseGUID]) REFERENCES ProgrammeCourse([ProgrammeCourseGUID]),
    CONSTRAINT fk_course_timetable_course FOREIGN KEY ([SessionGUID]) REFERENCES Session([SessionGUID]),
    CONSTRAINT fk_course_timetable_course FOREIGN KEY ([GroupGUID]) REFERENCES [Group]([GroupGUID]),
    CONSTRAINT fk_course_timetable_course FOREIGN KEY ([StaffGUID]) REFERENCES [Staff]([StaffGUID])
);

CREATE TABLE [GroupStudentList] (
	[GroupStudentListGUID] UNIQUEIDENTIFIER NOT NULL,
    [GroupGUID] UNIQUEIDENTIFIER NOT NULL,
    [StudentGUID] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [pk_groupStudentList] PRIMARY KEY CLUSTERED ([GroupStudentListGUID] ASC),
    CONSTRAINT [fk_group_groupStudentList] FOREIGN KEY ([GroupGUID]) REFERENCES [dbo].[Group] ([GroupGUID]),
    CONSTRAINT [fk_student_groupStudentList] FOREIGN KEY ([StudentGUID]) REFERENCES [dbo].[Student] ([StudentGUID])
);

CREATE TABLE [dbo].[Group] (
    [GroupGUID]     UNIQUEIDENTIFIER NOT NULL,
    [GroupNo]       INT              NOT NULL,
    [GroupCapacity] INT              NOT NULL,
    CONSTRAINT [pk_group] PRIMARY KEY CLUSTERED ([GroupGUID] ASC)
);

CREATE TABLE [Attendance] (
	[AttendanceGUID] UNIQUEIDENTIFIER NOT NULL,
    [ExamTimetableGUID] UNIQUEIDENTIFIER NULL,
    [StudentGUID] UNIQUEIDENTIFIER NOT NULL,
	[AttendanceCategory] VARCHAR(10) NOT NULL,
	[AttendanceStatus] VARCHAR(30) NOT NULL,
    [AttendanceDateTime] DATETIME NOT NULL,
    [LastUpdateDate] DATETIME NOT NULL,
    CONSTRAINT pk_attendance PRIMARY KEY (AttendanceGUID),
	CONSTRAINT fk_examtimetable_attendance FOREIGN KEY ([ExamTimetableGUID]) REFERENCES ExamTimetable([ExamTimetableGUID]),
	CONSTRAINT fk_student_attendance FOREIGN KEY ([StudentGUID]) REFERENCES Student([StudentGUID]),
);

CREATE TABLE [dbo].[Semester] (
    [SemesterGUID] UNIQUEIDENTIFIER NOT NULL,
    [SemesterYear] VARCHAR (2)      NOT NULL,
    [SemesterSem]  VARCHAR (2)      NOT NULL,
    [SemesterType] VARCHAR (10)     NOT NULL,
    CONSTRAINT [pk_semester] PRIMARY KEY CLUSTERED ([SemesterGUID] ASC)
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
    CONSTRAINT [fk_student_resetpassowrd] FOREIGN KEY ([StudentGUID]) REFERENCES [dbo].[Student] ([StudentGUID]),
    CONSTRAINT [fk_staff_resetpassowrd] FOREIGN KEY ([StaffGUID]) REFERENCES [dbo].[Staff] ([StaffGUID])
);

CREATE TABLE [dbo].[Notification] (
    [NotificationGUID]         UNIQUEIDENTIFIER    NOT NULL,
    [UserGUID]               UNIQUEIDENTIFIER     NOT NULL,
    [NotificationSubject]   VARCHAR (50)        NOT NULL,
    [NotificationContent]     VARCHAR (500)         NOT NULL,
    [ReadFlag]                CHAR (1)            NOT NULL,
    [SentDate]                   DATETIME            NOT NULL,
    [SentBy]                  VARCHAR (50)            NULL,
    [NotificationDescription]     VARCHAR (300)           NULL,
    PRIMARY KEY CLUSTERED ([NotificationGUID] ASC)
);

CREATE TABLE [dbo].[ProgrammeCourse] (
    [ProgrammeCourseGUID]   UNIQUEIDENTIFIER    NOT NULL,
    [CourseGUID]            UNIQUEIDENTIFIER    NOT NULL,
    [SemesterGUID]          UNIQUEIDENTIFIER    NOT NULL,
    [ProgrammeGUID]         UNIQUEIDENTIFIER    NOT NULL,
    [SessionMonth]          INT                 NOT NULL,
    PRIMARY KEY CLUSTERED ([ProgrammeCourseGUID] ASC),
    CONSTRAINT [fk_course_progCourse] FOREIGN KEY ([CourseGUID]) REFERENCES [dbo].[Course] ([CourseGUID]),
    CONSTRAINT [fk_semester_progCourse] FOREIGN KEY ([SemesterGUID]) REFERENCES [dbo].[Semester] ([SemesterGUID]),
    CONSTRAINT [fk_programme_progCourse] FOREIGN KEY ([ProgrammeGUID]) REFERENCES [dbo].[Programme] ([ProgrammeGUID])
);

CREATE TABLE [dbo].[Session] (
    [SessionGUID] UNIQUEIDENTIFIER    NOT NULL,
    [SessionYear] INT NOT NULL,
    [SessionMonth] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([SessionGUID])
);

CREATE TABLE [dbo].[AcademicCalender] (
    [AcademicCalenderGUID] UNIQUEIDENTIFIER    NOT NULL,
    [SessionGUID] UNIQUEIDENTIFIER    NOT NULL,
    [CalenderName] VARCHAR(100) NOT NULL,
    [CalenderType] VARCHAR(100) NOT NULL,
    [SemesterStartDate] DATE NOT NULL,
    [SemesterEndDate] DATE NOT NULL,
    [SemesterStudyDuration] INT NOT NULL,
    [SemesterExaminationDuration] INT NOT NULL,
    [SemesterBreakDuration] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([AcademicCalenderGUID] ASC),
    CONSTRAINT [fk_session_academicCalender] FOREIGN KEY ([SessionGUID]) REFERENCES [dbo].[Session] ([SessionGUID]),
);

CREATE TABLE [dbo].[Assessment] (
    [AssessmentGUID]  UNIQUEIDENTIFIER NOT NULL,
    [StaffGUID]       UNIQUEIDENTIFIER NOT NULL,
    [GroupGUID]       UNIQUEIDENTIFIER NOT NULL,
    [SessionGUID]     UNIQUEIDENTIFIER NOT NULL,
    [AssessmentTitle] VARCHAR (50)     NOT NULL,
    [AssessmentDesc]  VARCHAR (150)    NOT NULL,
    [DueDate]         DATETIME         NOT NULL,
    [CreatedDate]     DATETIME         NOT NULL,    
    [LastUpdateDate]  DATETIME         NULL,
	[UploadMaterials] VARCHAR (500) NULL,
    CONSTRAINT [pk_assessment] PRIMARY KEY CLUSTERED ([AssessmentGUID] ASC),
    CONSTRAINT [fk_staff_assessment] FOREIGN KEY ([StaffGUID]) REFERENCES [dbo].[Staff] ([StaffGUID]),
    CONSTRAINT [fk_group_assessment] FOREIGN KEY ([GroupGUID]) REFERENCES [dbo].[Group] ([GroupGUID]),
    CONSTRAINT [fk_session_assessment] FOREIGN KEY ([SessionGUID]) REFERENCES [dbo].[Session] ([SessionGUID])
);

CREATE TABLE [dbo].[CurrentSessionSemester] (
    [CurrentSessionSemesterGUID] UNIQUEIDENTIFIER NOT NULL,
    [SessionGUID] UNIQUEIDENTIFIER NOT NULL,
    [SemesterGUID] UNIQUEIDENTIFIER NOT NULL,
    [StudentGUID] UNIQUEIDENTIFIER NOT NULL,
    [Status] VARCHAR(20) NULL,
    [Reason] VARCHAR(100) NULL,
    PRIMARY KEY CLUSTERED ([CurrentSessionSemesterGUID] ASC),
    CONSTRAINT [fk_currentSessionSemester_session] FOREIGN KEY ([SessionGUID]) REFERENCES [dbo].[Session] ([SessionGUID]),   
    CONSTRAINT [fk_currentSessionSemester_semester] FOREIGN KEY ([SemesterGUID]) REFERENCES [dbo].[Semester] ([SemesterGUID]),
    CONSTRAINT [fk_currentSessionSemester_student] FOREIGN KEY ([StudentGUID]) REFERENCES [dbo].[Student] ([StudentGUID]),
);

CREATE TABLE [dbo].[ExamInvigilatorsList] (
    [ExamInvigilatorsListGUID] UNIQUEIDENTIFIER NOT NULL,
    [StaffGUID] UNIQUEIDENTIFIER NOT NULL,
    [ExamTimetableGUID] UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([ExamInvigilatorsListGUID] ASC),
    CONSTRAINT [fk_examInviList_staff] FOREIGN KEY ([StaffGUID]) REFERENCES [dbo].[Staff] ([StaffGUID]),   
    CONSTRAINT [fk_examInviList_examtimetable] FOREIGN KEY ([ExamTimetableGUID]) REFERENCES [dbo].[ExamTimetable] ([ExamTimetableGUID]),
);

CREATE TABLE [dbo].[MeetingLink] (
    [MeetingLinkGUID] UNIQUEIDENTIFIER NOT NULL,
    [RoomTopic]       VARCHAR (100)    NOT NULL,
    [RoomID]          VARCHAR (100)    NOT NULL,
    [RoomPass]        VARCHAR (100)    NOT NULL,
    [CreatedDate]     DATETIME         NOT NULL,
    CONSTRAINT [pk_meetinglink] PRIMARY KEY CLUSTERED ([MeetingLinkGUID] ASC)
);

