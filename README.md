# TLIP-151E
 
When working on Unity, any two people working on the same scene (e.g. World Scene) will lead to a conflict. Ensure that all co-workers are working on separate scenes at any given time.

General Workflow:

(1) Git Folder Hierarchy
There shall be multiple Unity project folders and other folders.
/TLIP-151E
	//AeroGO_alpha
	//AeroGO_beta
	//Misc Assets

For Git to be able to commit, each file must be <50MB.

(2) General Workflow
Test functionalities using Unity Project Test. Upon review, integrate to Unity Project Dev. Assets like characters, sprites, 3D models will be loacted in Misc Assets folder.

(3) Commit
Work on one function at a time (i.e. don't work on another funcitnoality until current task is done). Only after finishing current task, make commit, and then move onto next task. Task list shall be reviewed and revised each day.