1.
After you create new project WPFUI
Right click References(WPFUI project) > Manage NuGet Packages...
	Browse
	Caliburn.Micro (Rob Eisenberg)
	Caliburn.Micro.Core (Rob Eisenberg)

2.
Open App.xaml 
~~~~~~~~~~~~~
	StartupUri="MainWindow.xaml"
	means we start the application using MainWindow.xaml
	delete MainWindow.xaml File!
		because we want caliburn.micro
	delete StartupUri="MainWindow.xaml"

3.	
App.xaml: 
~~~~~~~~~~
Replace content or just add the Application.Resources part

<Application x:Class="WPFUI.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:local="clr-namespace:WPFUI"
             >
    <Application.Resources>
         <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary>
                    <local:Bootstrapper x:Key="Bootstrapper" />
                </ResourceDictionary>
            </ResourceDictionary.MergedDictionaries>
         </ResourceDictionary>
    </Application.Resources>
</Application>
this tells the WPF to start with Bootstrapper from class Bootstrapper.cs

4.
So ... lets create Bootstrapper.cs:
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;

namespace WPFUI
{
    /// <summary>
    /// Starting point of our application
    /// </summary>
    class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize(); // starts up some processes
        }

        // OnStartUp event
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>(); // displaying the root view of this model
        }
    }
}
	
5.
Create Models folder in project
	This is the data
Create Views folder in project
	This is the UI - objects with XAML : textbox, textblock, etc.
Create ViewsModels folder in project
	This is the drivers behind the application here the code lives 
	which supports the views behind the scenes
	this is the code part which connects between View and Models
This is MVVM -> Model, View, ViewModel
So ...
Where would ShellViewModel will reside?
	Answer: in the ViewsModel folder
	
6.
Create ShellViewModel class in ViewsModel	
ShellViewModel : Screen --> adds functionality like
when you close it asks you: "Are you sure you wanna close?"

7.
We need corresponding View
Views > Add > Window > call it ShellView.xaml

8.
Run!

9.
what is running?
The ShellView.xaml
Open ShellView.xaml - add Background="Aqua"
Run again!
see? told ya...
Remove the aqua

10. 
what happen here?
how did he connected?
App.xaml -> calls Bootstrapper -> calls ShellViewModel
but who calls ShellView.xaml?????
Answer: naming convention
the actual name is Shell!, therefor:
view = ShellView
view-model = ShellViewModel

11.
Add first name property in ShellViewModel.cs:
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//...
namespace WPFUI.ViewModels
{
    public class ShellViewModel : Screen
    {
        private string _firstName;

        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
            }
        }
    }
}

 12.
 ShellView.xaml
 ~~~~~~~~~~~~~~
 <Window x:Class="WPFUI.Views.ShellView"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:WPFUI.Views"
        mc:Ignorable="d" FontSize="18"
        Title="ShellView" Height="300" Width="300" WindowStartupLocation="CenterScreen" >
    <Grid>
	
		<!-- Click here Window (first line) + F4 > WindowStartUp > CenterScreen -->
	
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="20" />
            <ColumnDefinition Width="auto" />
            <ColumnDefinition Width="auto" />
            <ColumnDefinition Width="auto" />
            <ColumnDefinition Width="auto" />
            <ColumnDefinition Width="*" />
            <ColumnDefinition Width="20" />
        </Grid.ColumnDefinitions>
        <Grid.RowDefinitions>
            <RowDefinition Height="20" />
            <RowDefinition Height="auto" />
            <RowDefinition Height="auto" />
            <RowDefinition Height="auto" />
            <RowDefinition Height="auto" />
            <RowDefinition Height="auto" />
            <RowDefinition Height="*" />
            <RowDefinition Height="20" />
        </Grid.RowDefinitions>
        
        <!-- 1. start with this + also add FontSize=18 in Window (strat page here!) 
        <TextBlock Grid.Row="1" Grid.Column="1">
            Testing
        </TextBlock>
        -->
        
        <!-- 2. 
        <TextBlock Grid.Row="1" Grid.Column="1" x:Name="FirstName">
        </TextBlock>
        -->
        <!-- Run - nothing happens? why?
                because FirstName is empty:
                Go to ShellViewModel.cs
                set _firstName = "Itay" 
                Now run Again!!
                It shows the name since the ViewModel is the backend code!
                Draw a Diagram
        -->
           
        <!-- 3. this will not work since FirstName is already in used
                so how do I connect TextBlock and TextBox into the same value
                you cannot, not like this
                you could modify TextBlock x:Name="FirstName2"
                and then here you can change it to FirstName,
                and you will have the TextBox with FirstName an empty TextBlock
        -->
        <TextBox MinWidth="100" Grid.Row="2" Grid.Column="1" x:Name="FirstName"/>

        <!-- 4. Modify the TextBlock, at 2. 
                Add:
                Text="{Binding Path=FirstName, Mode=OneWay}"
                Remove x:Name="FirstName"
                OneWay binding means: it will not get new input
                Run, change text-box, the text block will not change!!
                We need notify property change,
                set it up at SHellViewModel.cs!!
        -->
        <TextBlock Grid.Row="1" Grid.Column="1" 
                   Text="{Binding Path=FirstName, Mode=OneWay}"
        >
        </TextBlock>

       
    </Grid>
</Window>

 13.
 ShellViewModel.cs:
 ~~~~~~~~~~~~~~~~~~
namespace WPFUI.ViewModels
{
    public class ShellViewModel : Screen
    {
        private string _firstName = "Itay";

        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                
                // notify that the FirstName property has changed!
                NotifyOfPropertyChange(() => FirstName);
            }
        }
    }
}

14. 
Run
now, when you modify the TextBox the TextBlock will change!!!

15.
Add last-name/full-name property:

ShellViewModel.cs:
~~~~~~~~~~~~~~~~~~
namespace WPFUI.ViewModels
{
    public class ShellViewModel : Screen
    {
        private string _firstName = "Itay";

        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                
                // notify that the FirstName property has changed!
                NotifyOfPropertyChange(() => FirstName);
                NotifyOfPropertyChange(() => FullName);
            }
        }

        private string _lastName = "Hauptman";
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;

                // notify that the FirstName property has changed!
                NotifyOfPropertyChange(() => LastName);
                NotifyOfPropertyChange(() => FullName);
            }
        }

        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
            
        }
    }
}

16.
ShellView.xaml
~~~~~~~~~~~~~~
<Window x:Class="WPFUI.Views.ShellView"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:WPFUI.Views"
        mc:Ignorable="d" FontSize="18"
        Title="ShellView" Height="300" Width="300" WindowStartupLocation="CenterScreen" >
    <Grid>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="20" />
            <ColumnDefinition Width="auto" />
            <ColumnDefinition Width="auto" />
            <ColumnDefinition Width="auto" />
            <ColumnDefinition Width="auto" />
            <ColumnDefinition Width="*" />
            <ColumnDefinition Width="20" />
        </Grid.ColumnDefinitions>
        <Grid.RowDefinitions>
            <RowDefinition Height="20" />
            <RowDefinition Height="auto" />
            <RowDefinition Height="auto" />
            <RowDefinition Height="auto" />
            <RowDefinition Height="auto" />
            <RowDefinition Height="auto" />
            <RowDefinition Height="*" />
            <RowDefinition Height="20" />
        </Grid.RowDefinitions>

        <!-- Click here Window (first line) + F4 > WindowStartUp > CenterScreen -->

        <!-- 1. start with this + also add FontSize=18 in Window (strat page here!) 
        <TextBlock Grid.Row="1" Grid.Column="1">
            Testing
        </TextBlock>
        -->
        
        <!-- 2. 
        <TextBlock Grid.Row="1" Grid.Column="1" x:Name="FirstName">
        </TextBlock>
        -->
        <!-- Run - nothing happens? why?
                because FirstName is empty:
                Go to ShellViewModel.cs
                set _firstName = "Itay" 
                Now run Again!!
                It shows the name since the ViewModel is the backend code!
                Draw a Diagram
        -->
           
        <!-- 3. this will not work since FirstName is already in used
                so how do I connect TextBlock and TextBox into the same value
                you cannot, not like this
                you could modify TextBlock x:Name="FirstName2"
                and then here you can change it to FirstName,
                and you will have the TextBox with FirstName an empty TextBlock
        -->
        <TextBox MinWidth="100" Grid.Row="2" Grid.Column="1" x:Name="FirstName"/>

        <!-- 4. Modify the TextBlock, at 2. 
                Add:
                Text="{Binding Path=FirstName, Mode=OneWay}"
                Remove x:Name="FirstName"
                OneWay binding means: it will not get new input
                Run, change text-box, the text block will not change!!
                We need notify property change,
                set it up at SHellViewModel.cs!!
        -->
        <TextBlock Grid.Row="1" Grid.Column="1" 
                   Text="{Binding Path=FirstName, Mode=OneWay}"
        >
        </TextBlock>


        <!-- 5 -->
        <TextBox MinWidth="100" Grid.Row="2" Grid.Column="2" x:Name="LastName"/>

        <TextBlock Grid.Row="1" Grid.Column="2" 
                   Text="{Binding Path=LastName, Mode=OneWay}"
        >
        </TextBlock>

        <TextBlock Grid.Row="1" Grid.Column="3" 
                   Text="{Binding Path=FullName, Mode=OneWay}"
        >
        </TextBlock>
        <!-- when you write the text-block column expands!!! -->
        <!-- you can ADD: Grid.ColumnSpan="2" -->
        <!-- you can ADD: MaxWidth -->

        
        
        
    </Grid>
</Window>

17.
Models > Add class PersonModel
PersonModel.cs
~~~~~~~~~~~~~~
namespace WPFUI.Models
{
    public class PersonModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

18.
ShellViewModel.cs:
~~~~~~~~~~~~~~~~~~
namespace WPFUI.ViewModels
{
    public class ShellViewModel : Screen
    {
        private string _firstName = "Itay";
        private string _lastName = "Hauptman";

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;

                // notify that the FirstName property has changed!
                NotifyOfPropertyChange(() => FirstName);
                NotifyOfPropertyChange(() => FullName);
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;

                // notify that the FirstName property has changed!
                NotifyOfPropertyChange(() => LastName);
                NotifyOfPropertyChange(() => FullName);
            }
        }

        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }

        }

        private BindableCollection<PersonModel> _people = new BindableCollection<PersonModel>(); // avoid crash

        public BindableCollection<PersonModel> People
        {
            get
            {
                return _people;
            }
        }


        private PersonModel _selectedPerson = new PersonModel();

        public PersonModel SelectedPerson
        {
            get
            {
                return _selectedPerson;
            }
            set
            {
                _selectedPerson = value;
                NotifyOfPropertyChange(() => SelectedPerson);
            }
        }

        // add ctor
        public ShellViewModel()
        {
            People.Add(new PersonModel { FirstName = "Dana", LastName = "Mor" });
            People.Add(new PersonModel { FirstName = "David", LastName = "Shlomo" });
            People.Add(new PersonModel { FirstName = "Maya", LastName = "Cohen" });
            People.Add(new PersonModel { FirstName = "Samantha", LastName = "Benet" });
        }
    }
}

19.
ShellView.xaml
~~~~~~~~~~~~~~
<Window x:Class="WPFUI.Views.ShellView"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:WPFUI.Views"
        mc:Ignorable="d" FontSize="18"
        Title="ShellView" Height="300" Width="300" WindowStartupLocation="CenterScreen" >
    <Grid>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="20" />
            <ColumnDefinition Width="auto" />
            <ColumnDefinition Width="auto" />
            <ColumnDefinition Width="auto" />
            <ColumnDefinition Width="auto" />
            <ColumnDefinition Width="*" />
            <ColumnDefinition Width="20" />
        </Grid.ColumnDefinitions>
        <Grid.RowDefinitions>
            <RowDefinition Height="20" />
            <RowDefinition Height="auto" />
            <RowDefinition Height="auto" />
            <RowDefinition Height="auto" />
            <RowDefinition Height="auto" />
            <RowDefinition Height="auto" />
            <RowDefinition Height="*" />
            <RowDefinition Height="20" />
        </Grid.RowDefinitions>

        <!-- Click here Window (first line) + F4 > WindowStartUp > CenterScreen -->

        <!-- 1. start with this + also add FontSize=18 in Window (strat page here!) 
        <TextBlock Grid.Row="1" Grid.Column="1">
            Testing
        </TextBlock>
        -->
        
        <!-- 2. 
        <TextBlock Grid.Row="1" Grid.Column="1" x:Name="FirstName">
        </TextBlock>
        -->
        <!-- Run - nothing happens? why?
                because FirstName is empty:
                Go to ShellViewModel.cs
                set _firstName = "Itay" 
                Now run Again!!
                It shows the name since the ViewModel is the backend code!
                Draw a Diagram
        -->
           
        <!-- 3. this will not work since FirstName is already in used
                so how do I connect TextBlock and TextBox into the same value
                you cannot, not like this
                you could modify TextBlock x:Name="FirstName2"
                and then here you can change it to FirstName,
                and you will have the TextBox with FirstName an empty TextBlock
        -->
        <TextBox MinWidth="100" Grid.Row="2" Grid.Column="1" x:Name="FirstName"/>

        <!-- 4. Modify the TextBlock, at 2. 
                Add:
                Text="{Binding Path=FirstName, Mode=OneWay}"
                Remove x:Name="FirstName"
                OneWay binding means: it will not get new input
                Run, change text-box, the text block will not change!!
                We need notify property change,
                set it up at SHellViewModel.cs!!
        -->
        <TextBlock Grid.Row="1" Grid.Column="1" 
                   Text="{Binding Path=FirstName, Mode=OneWay}"
        >
        </TextBlock>


        <!-- 5 -->
        <TextBox MinWidth="100" Grid.Row="2" Grid.Column="2" x:Name="LastName"/>

        <TextBlock Grid.Row="1" Grid.Column="2" 
                   Text="{Binding Path=LastName, Mode=OneWay}"
        >
        </TextBlock>

        <TextBlock Grid.Row="1" Grid.Column="3" 
                   Text="{Binding Path=FullName, Mode=OneWay}"
        >
        </TextBlock>
        <!-- when you write the text-block column expands!!! -->
        <!-- you can ADD: Grid.ColumnSpan="2" -->
        <!-- you can ADD: MaxWidth -->

        <!-- Better to add comment saying
            Row 1
            Row 2
            Much easier to read!!
        
            OneWayToSource = go from this from ==> to the property
            the opposite of OneWay (which is from the property to here)
        -->
        
        <!-- 6 Combo box -->
        <ComboBox Grid.Row="1" Grid.Column="1" 
                  x:Name="People" 
                  SelectedItem="{Binding Path=SelectedPerson, Mode=OneWayToSource}"
                  DisplayMemberPath="FirstName" />
        
        <!-- Now lets add the selected person last name 
             user underscore _
             [name]_[property]
        -->
        <TextBlock Grid.Row="3" Grid.Column="2" 
                   x:Name="SelectedPerson_LastName"
                   ></TextBlock>       
    </Grid>
</Window>

20.
Add ClearText button
ShellViewModel:
~~~~~~~~~~~~~~~
        public void ClearText()
        {
            FirstName = "";
            LastName = "";
        }
ShellView.xaml
~~~~~~~~~~~~~~
        <!-- add ClearText method to ShellViewModel 
             you only need the method name to be same as the Button x:Name
        -->
        <Button x:Name="ClearText" Grid.Row="4" Grid.Column="1">Clear Names</Button>
		
21.
what if we don't want the button to be enabled if there are no name? 
add CanClearText method in ShellViewModel - returns bool
And you don't need to change the XAML!!!!
The Can___ hooks up automatically
ShellViewModel:
~~~~~~~~~~~~~~~
        public void ClearText()
        {
            FirstName = "";
            LastName = "";
        }

        public bool CanClearText(string firstName, string lastName)
        {
            return !String.IsNullOrEmpty(firstName) || !String.IsNullOrEmpty(lastName);
        }

22.
Adding few views:
ShellViewModel inheritance-
multiple objects -> only one open
multiple objects -> multiple open
Simplest: one object/ child at a time
when we ask for child - it creates and put in view
when we change children it will destroy the old and create a new one
with the multiple it will close them but not destroy them (still in available objects list)

here we need one child
so we will do Conductor

so modify:
namespace WPFUI.ViewModels
{
    // Conductor == multiple forms
    public class ShellViewModel : Conductor<object>
	...
}

23.
In ViewModels:
Create class FirstChildViewModel : Screen
    class FirstChildViewModel : Screen
    {
    }
Create class SecondChildViewModel : Screen
    class SecondChildViewModel : Screen
    {
    }
	
24.
In View > Add > UserControl (WPF) 
User controls can be used alone, or inside other window
window cannot be inside other window

Add FirstChildView:
~~~~~~~~~~~~~~~~~~~
change color to blue:
<UserControl x:Class="WPFUI.Views.FirstChildView"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
             xmlns:d="http://schemas.microsoft.com/expression/blend/2008" 
             xmlns:local="clr-namespace:WPFUI.Views"
             mc:Ignorable="d" 
             d:DesignHeight="300" d:DesignWidth="300"
             Background="Blue">
    <Grid>
    </Grid>
</UserControl>

Add SecondChildView:
change color to green:
~~~~~~~~~~~~~~~~~~~
<UserControl x:Class="WPFUI.Views.SecondChildView"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
             xmlns:d="http://schemas.microsoft.com/expression/blend/2008" 
             xmlns:local="clr-namespace:WPFUI.Views"
             mc:Ignorable="d" 
             d:DesignHeight="300" d:DesignWidth="300"
             Background="Green"
             >
    <Grid>
            
    </Grid>
</UserControl>

25.
Now add to ShellViewModel.cs

namespace WPFUI.ViewModels
{
    // Conductor == multiple forms
    public class ShellViewModel : Conductor<object>
    {
        private string _firstName = "Itay";
        private string _lastName = "Hauptman";

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;

                // notify that the FirstName property has changed!
                NotifyOfPropertyChange(() => FirstName);
                NotifyOfPropertyChange(() => FullName);
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;

                // notify that the FirstName property has changed!
                NotifyOfPropertyChange(() => LastName);
                NotifyOfPropertyChange(() => FullName);
            }
        }

        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }

        }

        private BindableCollection<PersonModel> _people = new BindableCollection<PersonModel>(); // avoid crash

        public BindableCollection<PersonModel> People
        {
            get
            {
                return _people;
            }
        }


        private PersonModel _selectedPerson = new PersonModel();

        public PersonModel SelectedPerson
        {
            get
            {
                return _selectedPerson;
            }
            set
            {
                _selectedPerson = value;
                NotifyOfPropertyChange(() => SelectedPerson);
            }
        }

        // add ctor
        public ShellViewModel()
        {
            People.Add(new PersonModel { FirstName = "Dana", LastName = "Mor" });
            People.Add(new PersonModel { FirstName = "David", LastName = "Shlomo" });
            People.Add(new PersonModel { FirstName = "Maya", LastName = "Cohen" });
            People.Add(new PersonModel { FirstName = "Samantha", LastName = "Benet" });
        }

        public void ClearText()
        {
            FirstName = "";
            LastName = "";
        }

        public bool CanClearText(string firstName, string lastName)
        {
            return !String.IsNullOrEmpty(firstName) || !String.IsNullOrEmpty(lastName);
        }

        public void LoadPageOne()
        {
            ActivateItem(new FirstChildViewModel());
        }

        public void LoadPageTwo()
        {
            ActivateItem(new SecondChildViewModel());
        }
    }
}

26.
Add it to XAML

ShellView.XAML:
~~~~~~~~~~~~~~~
<Window x:Class="WPFUI.Views.ShellView"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:WPFUI.Views"
        mc:Ignorable="d" FontSize="18"
        Title="ShellView" Height="300" Width="300" WindowStartupLocation="CenterScreen" >
    <Grid>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="20" />
            <ColumnDefinition Width="auto" />
            <ColumnDefinition Width="auto" />
            <ColumnDefinition Width="auto" />
            <ColumnDefinition Width="auto" />
            <ColumnDefinition Width="*" />
            <ColumnDefinition Width="20" />
        </Grid.ColumnDefinitions>
        <Grid.RowDefinitions>
            <RowDefinition Height="20" />
            <RowDefinition Height="auto" />
            <RowDefinition Height="auto" />
            <RowDefinition Height="auto" />
            <RowDefinition Height="auto" />
            <RowDefinition Height="auto" />
            <RowDefinition Height="*" />
            <RowDefinition Height="20" />
        </Grid.RowDefinitions>

        <!-- Click here Window (first line) + F4 > WindowStartUp > CenterScreen -->

        <!-- 1. start with this + also add FontSize=18 in Window (strat page here!) 
        <TextBlock Grid.Row="1" Grid.Column="1">
            Testing
        </TextBlock>
        -->
        
        <!-- 2. 
        <TextBlock Grid.Row="1" Grid.Column="1" x:Name="FirstName">
        </TextBlock>
        -->
        <!-- Run - nothing happens? why?
                because FirstName is empty:
                Go to ShellViewModel.cs
                set _firstName = "Itay" 
                Now run Again!!
                It shows the name since the ViewModel is the backend code!
                Draw a Diagram
        -->
           
        <!-- 3. this will not work since FirstName is already in used
                so how do I connect TextBlock and TextBox into the same value
                you cannot, not like this
                you could modify TextBlock x:Name="FirstName2"
                and then here you can change it to FirstName,
                and you will have the TextBox with FirstName an empty TextBlock
        -->
        <TextBox MinWidth="100" Grid.Row="2" Grid.Column="1" x:Name="FirstName"/>

        <!-- 4. Modify the TextBlock, at 2. 
                Add:
                Text="{Binding Path=FirstName, Mode=OneWay}"
                Remove x:Name="FirstName"
                OneWay binding means: it will not get new input
                Run, change text-box, the text block will not change!!
                We need notify property change,
                set it up at SHellViewModel.cs!!
        -->
        <TextBlock Grid.Row="1" Grid.Column="1" 
                   Text="{Binding Path=FirstName, Mode=OneWay}"
        >
        </TextBlock>


        <!-- 5 -->
        <TextBox MinWidth="100" Grid.Row="2" Grid.Column="2" x:Name="LastName"/>

        <TextBlock Grid.Row="1" Grid.Column="2" 
                   Text="{Binding Path=LastName, Mode=OneWay}"
        >
        </TextBlock>

        <TextBlock Grid.Row="1" Grid.Column="3" 
                   Text="{Binding Path=FullName, Mode=OneWay}"
        >
        </TextBlock>
        <!-- when you write the text-block column expands!!! -->
        <!-- you can ADD: Grid.ColumnSpan="2" -->
        <!-- you can ADD: MaxWidth -->

        <!-- Better to add comment saying
            Row 1
            Row 2
            Much easier to read!!
        
            OneWayToSource = go from this from ==> to the property
            the opposite of OneWay (which is from the property to here)
        -->
        
        <!-- 6 Combo box -->
        <ComboBox Grid.Row="1" Grid.Column="1" 
                  x:Name="People" 
                  SelectedItem="{Binding Path=SelectedPerson, Mode=OneWayToSource}"
                  DisplayMemberPath="FirstName" />
        
        <!-- Now lets add the selected person last name 
             user underscore _
             [name]_[property]
        -->
        <TextBlock Grid.Row="3" Grid.Column="2" 
                   x:Name="SelectedPerson_LastName"
                   ></TextBlock>
        
        <!-- Row 4 -->
        <!-- add ClearText method to ShellViewModel 
             you only need the method name to be same as the Button x:Name
        -->
        <Button x:Name="ClearText" Grid.Row="4" Grid.Column="1">Clear Names</Button>
        <!-- what if we don't want the button to be enabled if there are no name? 
             add CanClearText method in ShellViewModel - returns bool
        -->

        <!-- Row 5 -->
        <Button x:Name="LoadPageOne" Grid.Row="5" Grid.Column="1">Load First Page</Button>
        <Button x:Name="LoadPageTwo" Grid.Row="5" Grid.Column="2">Load Second Page</Button>

        <!-- Row 6 -->
        <ContentControl Grid.Row="6" Grid.Column="1" Grid.ColumnSpan="5"
                        x:Name="ActiveItem" />
    </Grid>
</Window>

run - and stretch the size - you will see it covers the entire window ("*")