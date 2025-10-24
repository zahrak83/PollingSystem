using PollingSystem.Dtos;
using PollingSystem.Infrastructure;
using PollingSystem.Infrastructure.Repositories;
using PollingSystem.Interface.IRepositories;
using PollingSystem.Interface.IServices;
using PollingSystem.Services;
using Spectre.Console;


AppDbContext context = new AppDbContext();


ISurveyRepository surveyRepo = new SurveyRepository(context);
IQuestionRepository questionRepo = new QuestionRepository(context);
IOptionRepository optionRepo = new OptionRepository(context);
IVoteRepository voteRepo = new VoteRepository(context);
IAdminRepository adminRepo = new AdminRepository(context);
INormalUserRepository userRepo = new NormalUserRepository(context);
IUserSurveyRepository userSurvey = new UserSurveyRepository(context);


IAdminService adminService = new AdminService(surveyRepo, questionRepo, optionRepo, voteRepo);
INormalUserService userService = new NormalUserService(surveyRepo, voteRepo);
IAuthService authService = new AuthService(adminRepo, userRepo);
ISurveyService surveyService = new SurveyService(surveyRepo, voteRepo);
IQuestionService questionService = new QuestionService(questionRepo, optionRepo);
IOptionService optionService = new OptionService(optionRepo, voteRepo);
IUserSurveyService userSurveyService = new UserSurveyService(userSurvey);
IVoteService voteService = new VoteService(voteRepo);


while (true)
{
    Console.Clear();
    AnsiConsole.MarkupLine("[bold yellow]Welcome to Polling System[/]");
    Console.WriteLine();
    AnsiConsole.MarkupLine("[green]1)[/] Login as Admin");
    AnsiConsole.MarkupLine("[green]2)[/] Login as User");
    AnsiConsole.MarkupLine("[red]3)[/] Exit");
    Console.Write("\nEnter choice: ");
    var input = Console.ReadLine();

    if (input == "3") 
        break;

    bool isAdmin = input == "1";
    Console.Clear();

    Console.Write("Username: ");
    string username = Console.ReadLine();
    Console.Write("Password: ");
    string password = Console.ReadLine();

    try
    {
        var user = authService.Login(username, password, isAdmin);
        Console.Clear();

        if (isAdmin)
            AdminMenu(user);
        else
            UserMenu(user);
    }
    catch (Exception ex)
    {
        AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        Console.ReadKey();
    }
}

void AdminMenu(UserDto admin)
{
    while (true)
    {
        Console.Clear();
        AnsiConsole.MarkupLine($"[bold yellow]Admin Panel - {admin.FullName}[/]");
        AnsiConsole.MarkupLine("[green]1)[/] Create Survey");
        AnsiConsole.MarkupLine("[green]2)[/] Delete Survey");
        AnsiConsole.MarkupLine("[green]3)[/] View Survey Results");
        AnsiConsole.MarkupLine("[red]4)[/] Logout");
        Console.Write("\nEnter choice: ");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                CreateSurvey(admin.Id);
                break;

            case "2":
                DeleteSurvey(admin.Id);
                break;

            case "3":
                ViewSurveyResults(admin.Id);
                break;

            case "4":
                return;

            default:
                AnsiConsole.MarkupLine("[red]Invalid option![/]");
                Console.ReadKey();
                break;
        }
    }
}

void CreateSurvey(int adminId)
{
    Console.Clear();
    Console.Write("Enter survey title: ");
    string title = Console.ReadLine();

    var questions = new List<CreateQuestionDto>();

    while (true)
    {
        Console.Clear();
        Console.Write("Enter question text: ");
        string qText = Console.ReadLine();

        var options = new List<CreateOptionDto>();
        for (int i = 1; i <= 4; i++)
        {
            Console.Write($"Enter option {i}: ");
            string optText = Console.ReadLine();
            options.Add(new CreateOptionDto
            {
                Text = optText
            });
        }

        questions.Add(new CreateQuestionDto 
        {
            Text = qText,
            Options = options
        });

        Console.Write("Add another question? (y/n): ");
        if (Console.ReadLine()?.ToLower() != "y")
            break;
    }

    try
    {
        var dto = new CreateSurveyDto 
        { 
            Title = title, 
            AdminId = adminId,
            Questions = questions
        };
        var result = adminService.CreateSurvey(dto);
        AnsiConsole.MarkupLine($"[green]Survey '{result.Title}' created successfully![/]");
    }
    catch (Exception ex)
    {
        AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
    }
    Console.ReadKey();
}

void DeleteSurvey(int adminId)
{
    Console.Clear();
    var surveys = adminService.GetSurveyByAadmin(adminId);

    if (surveys.Count == 0)
    {
        AnsiConsole.MarkupLine("[red]No surveys found.[/]");
        Console.ReadKey();
        return;
    }

    var table = new Table();
    table.AddColumn("ID");
    table.AddColumn("Title");
    table.AddColumn("Participants");

    foreach (var s in surveys)
        table.AddRow(s.Id.ToString(), s.Title, s.TotalParticipants.ToString());
    AnsiConsole.Write(table);

    Console.Write("\nEnter survey ID to delete: ");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        try
        {
            adminService.DeleteSurvey(id, adminId);
            AnsiConsole.MarkupLine("[green]Survey deleted successfully![/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
    }
    else
        AnsiConsole.MarkupLine("[red]Invalid input.[/]");

    Console.ReadKey();
}

void ViewSurveyResults(int adminId)
{
    Console.Clear();
    var surveys = adminService.GetSurveyByAadmin(adminId);

    if (surveys.Count == 0)
    {
        AnsiConsole.MarkupLine("[red]No surveys found.[/]");
        Console.ReadKey();
        return;
    }

    var table = new Table();
    table.AddColumn("ID");
    table.AddColumn("Title");
    table.AddColumn("Participants");

    foreach (var s in surveys)
        table.AddRow(s.Id.ToString(), s.Title, s.TotalParticipants.ToString());
    AnsiConsole.Write(table);

    Console.Write("\nEnter survey ID to view results: ");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        try
        {
            var result = adminService.GetSurveyResults(id);
            Console.Clear();

            AnsiConsole.MarkupLine($"[yellow]Survey: {result.SurveyTitle}[/]");
            AnsiConsole.MarkupLine($"[blue]Total Participants: {result.TotalParticipants}[/]\n");

            foreach (var q in result.Questions)
            {
                AnsiConsole.MarkupLine($"[bold]{q.QuestionText}[/]");
                var qTable = new Table();
                qTable.AddColumn("Option");
                qTable.AddColumn("Votes");
                qTable.AddColumn("Percentage");

                foreach (var opt in q.Options.OrderByDescending(o => o.VoteCount))
                {
                    string bar = new string('█', (int)opt.Percentage / 5);
                    qTable.AddRow(opt.OptionText, opt.VoteCount.ToString(), $"{opt.Percentage:F1}% {bar}");
                }

                AnsiConsole.Write(qTable);
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
    }
    else
        AnsiConsole.MarkupLine("[red]Invalid input.[/]");

    Console.ReadKey();
}

void UserMenu(UserDto user)
{
    while (true)
    {
        Console.Clear();
        AnsiConsole.MarkupLine($"[bold yellow]User Panel - {user.FullName}[/]");
        AnsiConsole.MarkupLine("[green]1)[/] View Available Surveys");
        AnsiConsole.MarkupLine("[green]2)[/] Participate in a Survey");
        AnsiConsole.MarkupLine("[red]3)[/] Logout");
        Console.Write("\nEnter choice: ");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                ShowSurveys();
                break;

            case "2":
                ParticipateSurvey(user.Id);
                break;

            case "3":
                return;

            default:
                AnsiConsole.MarkupLine("[red]Invalid option![/]");
                Console.ReadKey();
                break;
        }
    }
}

void ShowSurveys()
{
    Console.Clear();
    try
    {
        var surveys = surveyService.GetAll();
        var table = new Table();
        table.AddColumn("ID");
        table.AddColumn("Title");
        table.AddColumn("Created By");
        table.AddColumn("Participants");

        foreach (var s in surveys)
            table.AddRow(s.Id.ToString(), s.Title, s.AdminName, s.TotalParticipants.ToString());
        AnsiConsole.Write(table);
    }
    catch (Exception ex)
    {
        AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
    }
    Console.ReadKey();
}

void ParticipateSurvey(int userId)
{
    Console.Clear();
    try
    {
        var surveys = surveyService.GetAll();

        var table = new Table();
        table.AddColumn("ID");
        table.AddColumn("Title");
        table.AddColumn("Created By");

        foreach (var s in surveys)
            table.AddRow(s.Id.ToString(), s.Title, s.AdminName);
        AnsiConsole.Write(table);

        Console.Write("\nEnter survey ID: ");
        if (!int.TryParse(Console.ReadLine(), out int surveyId))
        {
            AnsiConsole.MarkupLine("[red]Invalid input.[/]");
            Console.ReadKey();
            return;
        }

        if (userSurveyService.HasUserSurvey(userId, surveyId))
        {
            AnsiConsole.MarkupLine("[red]You have already completed this survey.[/]");
            Console.ReadKey();
            return;
        }

        var questions = questionService.GetBySurveyId(surveyId);
        foreach (var q in questions)
        {
            Console.Clear();
            AnsiConsole.MarkupLine($"[bold]{q.Text}[/]");
            var qTable = new Table();
            qTable.AddColumn("Option ID");
            qTable.AddColumn("Text");

            foreach (var opt in q.Options)
                qTable.AddRow(opt.Id.ToString(), opt.Text);
            AnsiConsole.Write(qTable);

            Console.Write("\nChoose option ID: ");
            int optId = int.Parse(Console.ReadLine());

            voteService.AddVote(new VoteDto
            {
                OptionId = optId,
                SurveyId = surveyId,
                UserId = userId
            });
        }

        userSurveyService.SetSurveyDone(userId, surveyId);
        AnsiConsole.MarkupLine("[green]Your responses have been recorded successfully![/]");
    }
    catch (Exception ex)
    {
        AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
    }
    Console.ReadKey();
}