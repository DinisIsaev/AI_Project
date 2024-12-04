# Project Setup and Running Instructions

This guide will walk you through the necessary steps to set up and run the project.

## Prerequisites

Make sure you have the following installed before proceeding:

- **Unity** version 6000.0.27f1
- **Python** version 3.10.11

## Steps to Run the Code

### 1. Install Unity
   - Install Unity version 6000.0.27f1.

### 2. Open Unity Hub
   - Open Unity Hub and select **Add Project from Disk**.
   - Choose the project folder.

### 3. Set Up Virtual Environment
   - Open the project folder in your terminal.
   - Check if **Python** is installed and ensure the version is 3.10.11.
   
   ```bash
   python --version
   ```
### 4. Create a Virtual Environment  
- Navigate to the folder of the project and activate the virtual environment.
```bash
cd PathToYourFolder
python -m venv venv
```
- Activate the virtual environment.
```bash
venv\Scripts\activate
```
### 5. Install Dependencies
- Ensure pip is correctly installed and updated
```bash
python -m pip install --upgrade pip
```
- Install the required packages for the project
```bash
pip install mlagents
```

### 6. Set up in Unity
- Go to Unity and select Package Manager from the Window tab.
- In the Package Manager, select Unity Registry.
- Search for the MLAgents package and install it.
- (In case sample scene is empty) In project files open Assets/Prefabs and drag "Env" prefab into the sample scene.
- Adjust camera to your liking

### 7. Start the Experiment
- Press Play in Unity.
- The agents should start moving according to what they have learned.
- If the agents don't start moving in Unity, check the console for any errors or missing packages.