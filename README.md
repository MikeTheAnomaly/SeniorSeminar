# SeniorSeminar
My Senior Seminar project, "Augmented Reality Battlefield Tours." Which provides a curated AR experience using terrain LiDAR data to produce accurate onsite recreations of Civil War Battles.
META Tour
Michael Hoff
Blackburn College

Project and documentation submitted in partial fulfillment 
of the requirements for the Bachelor of Arts 
degree in Computer Science 
Blackburn College, Illinois

May 2022




Abstract
As augmented reality (AR) technology continues to rapidly advance it is only a matter of time before applications and their design has to be updated to match the new paradigm. This paper discusses: (1) The application of augmented reality for in person Tours; (2) the creation of workflows to allow for grounded and theatrical AR experiences; (3) the use and creation of digital doubles in Augmented reality to provide enhanced physical and visual representations of objects. 


Abstract	2
META Tour	4
Background in Augmented Reality	4
Creation of Tour Experiences in ARFoundation	5
Creation of Digital Doubles	6
Curation of a Tour	8
Issues of Approach	9
Leaviating Issues	9
Conclusion	10
References	11
Introduction & Motivation	13
Practicum in the areas of computer science	14
Research Paper 1: “Learning to Match 2D Images and 3D LIDAR Point Clouds for Outdoor Augmented Reality,” by Weiquan Liu and Xiamen University	14
Research Paper 2: “An Augmented Reality-based Mobile Learning System to Improve Students’ Learning Achievements and Motivations in Natural Science Inquiry Activities,” by Tosti H. C. Chaing	15
Description of Project	17
Goals	17
Related Projects/Applications	17
Microsoft Ancient Olympia Augmented Reality Tour	17
ARKit Object tracking and space session management	18
Project Distinction	19
Project Specification	19
Requirements	19
Actors and Scenarios	19
Analysis Model	21
User View	21
Structural View (Absolutely will change)	23
Alternatives	26
Setbacks	29
Updated timeline	30
Application	32

META Tour
Imagine for a moment that someone is visiting a battlefield. Surrounded by land marks, preserved military equipment, statues, and signs. All things to remind the guest of a battle that happened long ago. Signs tell stories of grandeur, statues show moments that give honor to those who passed. But you can only postulate the life of the soldier this way. The use of augmented reality in this scenario however can show the experience of a soldier, and can show the story of courage. Augmented Reality offers the chance to replay events on the very ground they happened. However, in order to realize the prospects of augmented reality on battlefields much work needs to be done to accurately track the users position in respect to the battlefield. 
Background in Augmented Reality
	Augmented Reality in many aspects is still very much in its infancy. With varying ways tracking is handled device to device. A Iphone 12 uses a mix of feature point detection with the camera as well as a lidar sensor. An android phone relies solely on the camera with an AI that generates depth information. Dedicated AR devices like the hololens use an array of lidar sensors, multiple cameras, and IR illuminators to track the origination of the device. With so many different implementations of augmented reality it's no surprise that there have not been very many apps that heavily rely on it. 

Of the methods of augmented reality there are three key categories of tracking: 
(1) Location based tracking using common sensors like cameras, gyroscope, and gps to obtain localized AR presence.
(2) Marker Based AR uses markers like QR codes/images and detection algorithms to serve as a reference point for AR content.
(3) Simultaneous Localization and Mapping (SLAM) uses aforementioned sensors as well as machine learning algorithms to determine objects locations, facial features, edge and ground plane detection, and mesh reconstruction algorithms.

These 3 methods are commonly grouped in API’s accessible to developers of their platforms. For instance Google has ARCore as its primary AR API and Apple has ARKit as its API for augmented reality. Although many of the functionalities of API’s are similar their implementation is very different as well offering different features. This makes it challenging to develop universal APPs.
Luckily however with Unity game engines AR Foundation one application can be made using universal AR features that can be built for multiple platforms seamlessly. ARFoundation acts as an adapter api between the different platforms. 

Creation of Tour Experiences in ARFoundation
AR Foundation serves as an adapter to API’s like ARKit and ARCore and provides a solid augmented reality experience for most application use cases. However, since the goal of this application is to create a tour experience outside covering real world terrain at a distance, tracking the position of things like hills and buildings is not very feasible with AR Foundation alone as its features like plane detection and feature point matching are only useful for close range objects. In order to accurately place objects at a distance other algorithms must be used. Looking at once again common sensors of AR devices it becomes clear that the only sensor capable of long distance AR localization is the GPS sensor (with some caveats). Using the GPS location and equations like the Haversine formula one can calculate the distance an object is away from an observer using the object's longitude and latitude. 
This is where the Unity Asset AR+GPS Location comes into play. The package comes with Mapbox’s directions API allowing for swift and quick localization and placement of objects based on their known longitude and latitude. However many considerations for GPS accuracy must be taken into account when developing apps using GPS as a form of positioning. GPS sensors in phones are only accurate and often have over 5 meters of inaccuracy in good conditions and no reference to the user's altitude. This is why ARFoundation is still the primary way for placement of objects in close proximity as well as being responsible for tracking the users movement and orientation through 3d space. Objects are converted from latitude to longitude to Unity world positions and placed as trackable objects in the AR Foundation. 
In order to build the tour experience it's important to not only be able to accurately track the user but also to be able to incorporate the real world's geometry in the simulated environment. for the Augmented Reality objects to interact with. When an object is placed behind a building in real life, the user should not see the object in the AR view. This need for occlusion of augmented assets behind real world objects requires what's known as digital doubles. Digital doubles as the name would suggest are the same size and shape of their real world counterparts and allow for AR content to simulate around them. This means when an object is supposed to be obscured by a building in the real world, a virtual building is placed to occlude the AR content. 
Creation of Digital Doubles
	One challenge for long distance AR applications such as this tour is the land's topography itself. In order to place objects on the perceived ground the simulation needs to be aware of the ground location. But as mentioned before AR+GPS on phones does not provide accurate altitude measurements. As well if the land's topography includes hills that obscure content that must also be taken into account. 
	Luckily for both land topography and buildings the United States Geological Survey provides accurate lidar data of most of the north american continent. With accurate altitude and position data down to 3in there is more than enough information to generate a 3d model of the land and buildings to a high degree of accuracy. 
	
To create the 3d model from lidar data a student license of FME Workbench was acquired. FME Workbench is considered one of the industry standards for large batch data conversions for many industries. Using their node based transform tools it is trivial to generate a 3d model from given lidar data. Once the model is created it is imported into Blender, an open source modeling program, for cleanup and retopology to lower the poly count and clean up loose edge loops. The end result is a 1:1 reaction of the buildings and land of the desired location. 

Finally the creation of a see-through occlusion shader written in shaderlab for Unity is needed in order to hide objects behind a given topography model but not leave the object visible in the AR view. 

Curation of a Tour
	After obtaining both a digital double of the land's topography and the ability to place objects. All that comes left is the curation of a Tour, a task that is both easy and complex. Easy as it is as simple as standard use of Unity features, such as the timeline, animator, sound systems, and unity input systems. But also tidus and time consuming. A good way to speed up the process is with animation tools like motion capture suits. Event driven design is also a great way to increase the interactivity of the application allowing a user's actions to dictate the AR experience. Unity provides the flexibility to accomplish any directors dreams for AR.

Issues of Approach
One big downside to the accuracy of GPS is the inaccuracy of compass readings on phone based AR systems. Since in order to calculate the position of objects is relative to a users phone’s GPS position if the users compass reading is inaccurate then the AR content will be placed at an incorrect location. Since digitally doubling terrain is a goal this means that buildings are often completely incorrect despite being proper distance away from the user the initial rotation of the origin plane for the gps objects does not allow for propper digital doubling in the worst case.
Another issue is when GPS accuracy is low for longitude and latitude positioning. This leads to objects being offset from their correct location. AR+GPS gives accuracy readings for its GPS related components so when implementing GPS related content it is smart to warn a user when the GPS quality is low. 

Leaviating Issues
	One can recall the SLAM (simultaneous localization and mapping) is only one of the ways AR content can be initialized. A techinice like marker based AR tracking is often far more reliable. So incorporating a solution like marker based AR as a fallback or calibration method to GPS tracking is an easy way to address some of the unreliable nature of GPS. A simple way to calibrate is to assign a marker to a specific location (think a permanent install) with a given Longitude and latitude assigned to the key value pair. This way when a user scans the Marker a script can automatically take the location of the marker in Unity world space and the location of the unity ARTracker camera (the user's phone tracked in unity) and override the GPS location provided from the phone. This Tells AR+GPS a precise location and rotation of the user. Effectively calibrating the GPS for a time. 
Conclusion
	In order to create the future of AR Tour experiences, providing an accurate model of the real world is pivotal. Using methods such as Lidar topography mesh generation provides the curator with a digital double of the real world allowing for faster iteration of tour material. This combined with the proposition to use GPS tracking as a way to place AR content at a distance helps pave the way for the future of augmented tours.



References
Tosti H. C. Chiang, Stephen J. H. Yang, & Gwo-Jen Hwang. (2014). An Augmented Reality-based Mobile Learning System to Improve Students’ Learning Achievements and Motivations in Natural Science Inquiry Activities. Journal of Educational Technology & Society, 17(4), 352–365. http://www.jstor.org/stable/jeductechsoci.17.4.352
Paper 1:
Liu, Weiquan & Lai, Baiqi & Wang, Cheng & Bian, Xuesheng & Yang, Wentao & Xia, Yan & Lin, Xiuhong & Lai, Shang-Hong & Weng, Dongdong & Li, Jonathan. (2020). Learning to Match 2D Images and 3D LiDAR Point Clouds for Outdoor Augmented Reality. 655-656. 10.1109/VRW50115.2020.00178. 
Paper 2:
Tosti H. C. Chiang, Stephen J. H. Yang, & Gwo-Jen Hwang. (2014). An Augmented Reality-based Mobile Learning System to Improve Students’ Learning Achievements and Motivations in Natural Science Inquiry Activities. Journal of Educational Technology & Society, 17(4), 352–365. http://www.jstor.org/stable/jeductechsoci.17.4.352

Endicott, Sean. “Microsoft and Greece Bring Ancient Olympia to Life with Augmented Reality.” Windows Central, Windows Central, 12 Nov. 2021, https://www.windowscentral.com/microsoft-brings-ancient-greece-life-augmented-reality. 

TECHNOLOGIES, U.
Unity - Scripting API: SceneManager
In-text: (Technologies, 2021)
Your Bibliography: Technologies, U., 2021. Unity - Scripting API: SceneManager. [online] Docs.unity3d.com. Available at: <https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.html> [Accessed 17 December 2021].
Docs.unity-ar-gps-location.com. 2022. Guide | Unity AR+GPS Location Docs (v3.6.0). [online] Available at: <https://docs.unity-ar-gps-location.com/guide/> [Accessed 29 April 2022].


Senior Seminar
Michael Hoff
Proposal: Augmented Reality Battlefield Tours


Introduction & Motivation
Augmented reality like most emerging technologies has taken a while to find its place among consumers. Most applications end up feeling noval. However some consumer products like snapchat lenses have been proven a huge success. One area that is continually put on a pedestal is the learning application of augmented reality, however there are very few real world apps that push the boundaries of what can be done that goes beyond standard teaching methods. For instance, although there is some advantage in viewing a model of a cell in 3d, that can be done without the use of augmented reality. This research wishes to change the paradigm of augmented reality with a curated experience that taylors augmented reality to its full potential. Not displaying unrelated 3d content that disconnects the user from their current space. But instead ushering the student to connect real life spaces with virtual reconstructions of the space at a different time period. This research will be done to incorporate real terrain data of American battlefields with phone based Augmented reality. Battles will be animated and narrated to provide the best experience possible for the user. 
Practicum in the areas of computer science
This Augmented reality application will heavily rely on the computer science areas of Algorithms and Communication. By combining different SLAM (simultaneous localization and mapping) algorithms with lidar point cloud matching algorithms, the application will be able to obtain acurite scene geometry, allowing for realistic occlusion and longer distance augmented features while still retaining the local space’s characteristics. Therefore the use of these Algorithms is incredibly important and related to computer science in its whole. The second area of great importance to this application is communication, as mentioned above Augmented reality often disconnects the user from their environment. But by incorporating real life geometry data into the application  animations will appear more grounded. This is the difference between a 3d character appearing through a hill or being obscured by a hill with occlusion. The graphics should communicate the emotion and strategic element of the battles without imposing a user's ability to engage and immerse themselves in the content. Finally a user should be able to learn from the content with the augmented reality visuals helping cement information instead of distracting.
Research Paper 1: “Learning to Match 2D Images and 3D LIDAR Point Clouds for Outdoor Augmented Reality,” by Weiquan Liu and Xiamen University
A team at Fujian Key Laboratory of sensing and computing for smart city at Xiamen University introduced there end-to-end network, “Siam2D3D-Net” which takes 2d images and matches it with LiDAR point cloud data to create a virtual-real registration system for augmented reality. To do this the team introduced their own patch volume dataset of 2D and 3D lidar pairs to train a neural network. 
With a focus on outdoor environments which have sparser lidar datasets, they trained a image branch based on Spatial Transformer Network module and a Visual Geometry Group network, as well trained a point cloud branch which is a modified PointNet network. The data is then trained together with a Margin Based contrasted loss function, which operates on pairs of samples instead of individual data, perfect for the two image processing layers.
The experiment's results showed a successful image to LiDAR classification on data with high contrasting shapes, such as a stairs railing or a corner of a building, but failed to recognize data with more noise such as a potted plant. The paper showed success in matching the 2d images with Lidar points in an urban environment allowing for real time mapping of the euclidean space. 
This experant is very close to the research goal of Augmented Reality Battlefield Tours as both match 2d photos with LiDAR data. However, although this research might be useful for urban environments, since most Battlefields are large open fields the method for retrieving euclidean space will not be useful. As shown by the accuracy with plants in the paper which would coincide with fields of grass. Though much of the information in this paper will still be useful such as the 2D to LiDAR dataset to train neural networks provided by the research team. As well, the recommendations for STN and VGG image branches for image to data conversions. Finally now knowing how difficult the process of matching the LiDAR to fields will pose, providing multiple tracking systems in the application, such as a QR marker will help initial matching of the LiDAR data. 
Research Paper 2: “An Augmented Reality-based Mobile Learning System to Improve Students’ Learning Achievements and Motivations in Natural Science Inquiry Activities,” by Tosti H. C. Chaing 
The “Augmented Reality Based Mobile Learning…” journal by Tosti H. C. Chaing and the National Central University in Tao-Yuan Taiwan, shows the advantages of teaching and communicating with augmented reality for teaching and learning in education. The paper separated 57 fourth grade students in two classes. One that used current online learning tools while the other used an Augmented Reality based application. 

The Study found that the students who used Augmented Reality Approach learned quicker, were more engaged, showed higher motivation and attention to school material. The AR application created a cycle of learning by first asking the students to find something, allowing them to investigate, create comments and images for notes, share with other classmates and reflect on their learning as a class. By using AR for the ask and investigate stages it encouraged students to look around the environment asking questions and interact with the AR instructor. 
Although the BattleField tours will also target students it will primarily focus on older kids most likely middle school or high school students. However the cycle of ask, investigate, create, share, reflect, might still be a very effective tool for the interaction between the user and the AR content. In order to do this with elements of the civil war, certain models could be placed closer to the user to perform tasks like loading a musket or finding a military leader. Incorporating ways for users to take screenshots and share would also incorporate some of the learning from this article. Finally, allowing users to view content already seen in the tours in an archive would also help the learning process. Overall this paper backs up the hypothesis that incorporating AR to the learning process will lead to better understanding.


Description of Project
Goals
The project goals are as follows:
Match LiDAR dataset to real world geometry 
Build a application that stores curated tour narration 
Allows user to select and preview different tours
Get the location of the start of the tour
Allow user to scan a QR code to start tour on site
Be able to use Augmented Reality features without the QR remaining in view
Mixing plane detection and image detection to give second opinion structure for best accuracy in tracking
 Combine motion capture data with inverse kinematics to provide accurate recreation of soldiers fighting on the battlefield
Related Projects/Applications
Microsoft Ancient Olympia Augmented Reality Tour
This Research is similar to a recent application created by Microsoft that allows Augmented reality overlays of ancient greek architecture in Italy. Where users can tour the grounds of Ancient Olympia and explore how athletes trained for the olympic games. 

Users on site can manually overlay 3d reconstructions of the buildings over the relics using a phone app. As well inside the museum there is a curated tour using hololens. 

The research Microsoft has done uses similar technologies to my application. Using LiDAR to map out the area's physical characteristics. But it is unclear from their presentations if the augmented reality application on the phone is capable of overlaying the reconstructions without the use of manual interventions (the user lines up the objects manually) which is the main goal of this research for Battle Fields so that users interaction is more streamlined within the application.
ARKit Object tracking and space session management
Another closely related new research is embedded within part of the augmented reality API this project will use, is ARKit for IOS devices. A new Feature that is in development is the use of 3D object tracking using the new LiDAR cameras embedded in modern Iphones. This allows objects to be tracked in real time as well maintained space recognition allowing spaces to be stored in memory and initialized after reopening the app. 
Although being able to store spaces and reopen them would be ideal for Battlefield Tours. The technology is not available on all devices, with android being another target platform for this application. As well the lidar sensors on the Iphone are short ranged and a different algorithm would need to be made to match up the larger LiDAR data for the fields. 

Project Distinction
The Research for Augmented Reality BattleField Tours therefore will be quite distinct from other applications in circulation. This is because it will use standardized Augmented reality features that exist in Apple IOS, Android, and Hololens. But intends to merge both Plane Detection and Image QR code tracking to match real LiDAR terrain data to the application. As of now there is not a competitor to the space of curated BattleField Tour apps that provide a curated tour based on current location throughout the battlefield that also provide Augmented reality features. 
Project Specification
Requirements
The application should:
Run on any AR Foundation Device
Android, Apple, HoloLens
Have a Intuitive contextual UI
Created with Unity Game Engine with latest LTS
Support for .net core 2.0
Compile with IL2CP to support modern C# standards
Support Android 8.0+ for ARCore functionality
IOS 11 for ARKit feature set
Shaders for occlusion written in HLSL with support for OpenGL
Pause, Play, rewind tour experience
Animated visuals that support the audio descriptions
Actors and Scenarios
Scenario 1:
	Bill is a history buff. He frequents battlefields and spends money on tours. Bill finds the app from seeing a QR code on a sign near a plaque for the Antietam Creek. Bill downloads the app where the app asks for his location. After accepting the message the app notices he is near the plaque for Antietam Creek and prompts him to look for the QR marker with his phone. Bill points the camera at the marker and the battlefield is reconstructed in AR. A timeline appears at the bottom of the app with a play button for a curated tour of the creek. To which bill plays. Bill is engulfed in the story of antietam creek and watches the events unfold like never before. Bill finishes the presentation and is recommended to walk to bunker church not too far from his current location to start the next tour. Bill closes the app and walks to the church. 

Scenario 2:
Bob is a historical weapons buff. He frequients the application and knows that a certain general's sword was in one of the augmented reality tours he had taken. Bob opens the app and goes to the archive section. There he looks up the name of the general and finds his sword. Bob is able to activate AR mode with the sword and view the sword on his kitchen table, as well Bob is given a timeline at the bottom of the screen for him to be given a history lesson on the sword.

Scenario 3:
Timmy is a middle school student and is not very patient. He opens the app when instructed to by his teacher. Timmy starts the tour and gets bored of the Augmented reality content. He turns off the Augmented reality content so he can go for a walk. Timmy still needs to learn the information provided by the App so he leaves the audio playing. Timmy hears about the destruction of a house from a stray cannon and wants to see the animation play out. He pulls up the app again and moves the timeline back to the event, recalibrates Augmented reality and replays the scene. He then closes the app.
Analysis Model
User View
Two potential Users views
A single user environment where a user can scan the qr and interact with curated tours (MVP)
A group tour mode where a Tour guide can trigger events and animations for the tourists. 

Structural View (Absolutely will change)




Alternatives
The most nontrivial part of this application is the ability to use AR at the distance required to view a battlefield. The further away from the tracking point the more jitter will be evident with models at distance. If the application is not stable enough to view long distances with Augmented Reality a shift will have to be made to provide content in the local space of the user. This however can still have a major impact on the learning experience as well is still a challenge enough for a senior seminar project.

Another Alternative path that can be made is in the event that the AR portion works fine but the creation of soldiers on the battlefield proves too intense for cell phone processors with minimum optimisation. In this case the content will be shifted to less of an experience seeing soldiers and more of an experience seeing tactics. Such as troop movement arrows to indicate what tactics the generals used in the battlefield without showing soldiers moving. 


Paper 1:
Liu, Weiquan & Lai, Baiqi & Wang, Cheng & Bian, Xuesheng & Yang, Wentao & Xia, Yan & Lin, Xiuhong & Lai, Shang-Hong & Weng, Dongdong & Li, Jonathan. (2020). Learning to Match 2D Images and 3D LiDAR Point Clouds for Outdoor Augmented Reality. 655-656. 10.1109/VRW50115.2020.00178. 
Paper 2:
Tosti H. C. Chiang, Stephen J. H. Yang, & Gwo-Jen Hwang. (2014). An Augmented Reality-based Mobile Learning System to Improve Students’ Learning Achievements and Motivations in Natural Science Inquiry Activities. Journal of Educational Technology & Society, 17(4), 352–365. http://www.jstor.org/stable/jeductechsoci.17.4.352

Endicott, Sean. “Microsoft and Greece Bring Ancient Olympia to Life with Augmented Reality.” Windows Central, Windows Central, 12 Nov. 2021, https://www.windowscentral.com/microsoft-brings-ancient-greece-life-augmented-reality. 

TECHNOLOGIES, U.
Unity - Scripting API: SceneManager
In-text: (Technologies, 2021)
Your Bibliography: Technologies, U., 2021. Unity - Scripting API: SceneManager. [online] Docs.unity3d.com. Available at: <https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.html> [Accessed 17 December 2021].
Senior Seminar
Implementation of plan


Setbacks
Many unexpected challenges came to fruition when implementing the application. Many libraries that were considered in the original plan were deprecated or out of date of AR Foundation such as the Mapbox unity AR api, which was the original choice instead of the paid AR+GPS library. This pushed back development and led to having to take alternative paths above. Starting with the localized doubles using marker based detection in the first lab tour, until finding AR+GPS and getting a discount from the developer as a student. 
	Another unexpected issue that is still very prevalent is the accuracy of GPS. Leading to more development time in mitigating the problem leading to further set backs in the quality of the tour itself. This led to the cut of a “Tour guide” persona meaning options for tour guides to control the experience for a group of users and the networking portion of the project had to be cut.





Updated timeline





Senior Seminar
User manual



The application consists of two main states. The menu state where tours are selected and the Augmented reality camera state.


The menu state consists of a simple scroll rect with the different available tour experiences. The first option is a test of the digital doubles using a marker in the lab. The marker goes on the edge of the table near the wall close to the plab. 




The second is the AR+GPS created tour. A large button for showing different states of the digital double of the blackburn campus. As well as two methods of debugging, one for AR+GPS showing related sensor accuracy and another for unity debug console for easy access to logging in case of an error. 

For best results check the current ephemeris gps data in order to see the best time of day for a tour. Ephemeris gps data shows when satellites are positioned over a given location. The more satellites available the more accurate the gps location and therefore the tour experience. Its also a good idea to calibrate the phone's compass by creating a figure 8 pattern with the phone. 






To calibrate the gps after the initialization, place the Logo in the corner of Hudson (the object's pivot point) and scan the code with the phone. This will set the position of the digital double to the marker to allow for more accurate doubling of the buildings. 

When the world is initialized a play button will appear starting the tour.

