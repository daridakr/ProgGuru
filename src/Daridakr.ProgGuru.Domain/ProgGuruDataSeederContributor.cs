using Daridakr.ProgGuru.Entities.Groups;
using Daridakr.ProgGuru.Entities.Projects;
using Daridakr.ProgGuru.Entities.Users.Specializations;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;

namespace Daridakr.ProgGuru
{
    public class ProgGuruDataSeederContributor
        : IDataSeedContributor, ITransientDependency
    {
        private readonly IGuidGenerator _guidGenerator;

        private readonly IProjectRepository _projectRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly ISpecializationRepository _specializationRepository;

        private readonly ProjectManager _projectManager;
        private readonly GroupManager _groupManager;
        private readonly SpecializationManager _specializationManager;

        public ProgGuruDataSeederContributor(
            IGuidGenerator guidGenerator,
            IProjectRepository projectRepository,
            IGroupRepository groupRepository,
            ISpecializationRepository specializationRepository,
            ProjectManager projectManager,
            GroupManager groupManager,
            SpecializationManager specializationManager)
        {
            _guidGenerator = guidGenerator;
            _projectRepository = projectRepository;
            _groupRepository = groupRepository;
            _projectManager = projectManager;
            _groupManager = groupManager;
            _specializationManager = specializationManager;
            _specializationRepository = specializationRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            //if (await _groupRepository.GetCountAsync() <= 0)
            //{
            //    var unity = await _groupRepository.InsertAsync(
            //    await _groupManager.CreateAsync(
            //        ""
            //        "Unity",
            //        "Межплатформенная среда разработки компьютерных игр",
            //        "Unity — межплатформенная среда разработки компьютерных игр. Unity позволяет создавать приложения, работающие под более чем 20 различными операционными системами, включающими персональные компьютеры, игровые консоли, мобильные устройства, интернет-приложения и другие. Выпуск Unity состоялся в 2005 году и с того времени идёт постоянное развитие.  Основными преимуществами Unity являются наличие визуальной среды разработки, межплатформенной поддержки и модульной системы компонентов. К недостаткам относят появление сложностей при работе с многокомпонентными схемами и затруднения при подключении внешних библиотек.  На Unity написаны тысячи игр, приложений и симуляций, которые охватывают множество платформ и жанров.",
            //        "Unity Technologies",
            //        "2005",
            //        "",
            //        "unity.com"
            //    )
            //);

            //    var aspnet = await _groupRepository.InsertAsync(
            //        await _groupManager.CreateAsync(
            //            "ASP.NET MVC",
            //            "Фреймворк для создания веб-приложений, реализующий шаблон MVC",
            //            "Платформа ASP.NET MVC представляет собой фреймворк для создания сайтов и веб-приложений с помощью реализации паттерна MVC.  Концепция паттерна предполагает разделение приложения на три компонента:  Контроллер (controller) представляет класс, обеспечивающий связь между пользователем и системой, представлением и хранилищем данных. Он получает вводимые пользователем данные и обрабатывает их. И в зависимости от результатов обработки отправляет пользователю определенный вывод, например, в виде представления.  Представление (view) - это собственно визуальная часть или пользовательский интерфейс приложения. Как правило, html-страница, которую пользователь видит, зайдя на сайт.  Модель (model) представляет класс, описывающий логику используемых данных.",
            //            "Microsoft",
            //            "",
            //            "2009"
            //        )
            //    );
            //}

            //if (await _projectRepository.GetCountAsync() <= 0)
            //{
            //    var oscarProject = await _projectRepository.InsertAsync(
            //            await _projectManager.CreateAsync(
            //                Guid.Parse("56f9fadc-ebda-a15d-d32e-3a0200f67608"),
            //                Guid.Parse("02606336-0c8a-e290-129b-3a0200fbf6ef"),
            //                "Программная реализация мобильной игры «Oskarcat»",
            //                "Разработка казуальной аркадной игры для мобильных устройств на платформе Android. Данная игра повествует о жизни кота Оскара, который сталкивается с различными испытаниями и передрягами на протяжении всей жизни. Игра ориентирована на людей от 7 до 25 лет для забавы и веселого времяпровождения.",
            //                "Данная игра является казуальной, что означает у нее наличие широкого круга пользователей, так как казуальные игры отличаются простыми правилами и не требуют от пользователя особой усидчивости, затрат времени на обучение или каких-либо особых навыков. Привнесённые в игру элементы экшена, симулятора и квестов разбавляют ее новыми возможностями и характерными для данных жанров фичами, обычно не встречающихся в «чистых» платформерах, что делает игру уникальной и выделяет ее среди остальных, тем самым образуя новый, свой собственный жанр. Игровой процесс заключается в прохождении уровней и этапов игры, которые сопоставляются с периодами жизни игрового персонажа. Данная игра предназначена для развлечения и веселого времяпровождения, ориентированная на людей от 7 до 25 лет. Кроме того, Oscarcat предоставляет игроку интересную и, возможно, новую для него информацию, расширяя его познания о кошках. Подразумевается, что в процессе игры, игрок будет познавать этих животных с момента их появления на свет путём вызова ощущения себя на месте игрового персонажа. При разработке игры будет использована 2D графика с видом сбоку в соответствии с заданным сеттингом – мультяшный, события нашего времени. Данная игра является одиночной и может работать без подключения к интернету. Адаптивные элементы интерфейса позволят отображать и масштабировать игру под любой экран. Исходя из анализа жанра и сеттинга, было выявлено, что большая часть игроков является детьми и подростками. Соответственно, разработка игры направленна на данную аудиторию, и она является основной.",
            //                "ImageLink",
            //                ProjectCategory.Game,
            //                ProjectStatus.Abandoned,
            //                new DateTime(2020, 6, 8)
            //            )
            //        );

            //    var crowdProject = await _projectRepository.InsertAsync(
            //        await _projectManager.CreateAsync(
            //            Guid.Parse("56f9fadc-ebda-a15d-d32e-3a0200f67608"),
            //            Guid.Parse("29f606e1-d164-af30-1503-3a0200fbf7fb"),
            //            "Web-приложение «Краудфандинговая платформа»",
            //            "Разработка краудфандинговой платформы с организованным ведением базы данных, содержащей информацию о пользователях, кампаниях, новостях кампаний, рейтинга кампаний и пожертвованиях",
            //            "Доступ к Web-приложению будет ограничен, только авторизованные пользователи смогут просматривать и изменять информацию. Для групп пользователей будут организованы уровни доступа, в зависимости от которых будут предоставлены различные возможности в приложении. Организация рабочего процесса в приложении является довольно трудоемкой. Администратор приложения должен иметь возможность быстрого доступа информации и всех зарегистрированных пользователях, их кампаниях, всех доступных тематик для кампаний на сайте, редактирование информации о кампаниях. На основе изученной предметной области приложение должно выполнять следующие функции: –   авторизация администратора; –   ведение базы данных кампаний; –   регистрация пользователей; –   поиск определенных кампаний; Поскольку Web-приложение предполагает большое число операций по чтению, записи и изменению значительного объема данных, наиболее удобным вариантом будет включение в серверную часть технологий БД. Приложение будет создано с использованием базы данных SQL Server. SQL Server учитывает все современные требования по работе с данными различных форматов и из разнообразных источников и становится естественным выбором для построения платформы интеграции, управления и анализа любых данных. Аналогами веб-сайта являются другие краудфандинговые платформы Planeta.ru (https://planeta.ru/), Boomstarter (https://boomstarter.ru/). Данные сайты имеют весь необходимый и грамотно реализованный функционал, на который опирался разработчик при создании Web-приложения. Разрабатываемое Web-приложение имеет удобный и понятный интерфейс, простую для понимания функциональность и никакой рекламы.",
            //            "ImageLink",
            //            ProjectCategory.Web,
            //            ProjectStatus.Completed,
            //            new DateTime(2021, 7, 4),
            //            new DateTime(2021, 10, 5),
            //            "",
            //            "https://github.com/daridakr/Website-For-Crowdfunding-Campaigns"
            //        )
            //    );
            //}

            //if (await _tagRepository.GetCountAsync() <= 0)
            //{
            //    await _tagRepository.InsertAsync(
            //        new Tag(_guidGenerator.Create(), "abp")
            //    );

            //    await _tagRepository.InsertAsync(
            //        new Tag(_guidGenerator.Create(), "linux")
            //    );

            //    await _tagRepository.InsertAsync(
            //        new Tag(_guidGenerator.Create(), "фреймворк")
            //    );

            //    await _tagRepository.InsertAsync(
            //        new Tag(_guidGenerator.Create(), "удаленно")
            //    );
            //}

            //if(await _specializationRepository.GetCountAsync() <= 0)
            //{
            //    var cdeveloper = await _specializationRepository.InsertAsync(
            //            await _specializationManager.CreateAsync(
            //                Guid.Parse("56f9fadc-ebda-a15d-d32e-3a0200f67608"),
            //                "C# разработчик"
            //            )
            //        );

            //    var webDesigner = await _specializationRepository.InsertAsync(
            //            await _specializationManager.CreateAsync(
            //                Guid.Parse("56f9fadc-ebda-a15d-d32e-3a0200f67608"),
            //                "Web-дизайнер"
            //            )
            //        );
            //}
        }
    }
}
