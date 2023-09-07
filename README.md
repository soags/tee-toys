# TeeToys

CLI tools to "I'll work with temporary files for now" experience

## Installation

```powershell
dotnet tool install -g TeeToys
```

## Usage

### Create Temporary Folder

```powershell
t 
```

Create and open today's temporary folder. If it already exists, it will be opened.

Example. `C:\tmp\yyyy-MM-dd`

### Create Temporary Text File

```powershell
t txt
```

Create and open an empty text file in the temporary folder.
It will be opened with the application associated with .txt files.

Example. `C:\tmp\yyyy-MM-dd\yyyy-MM-dd_HHmmss.txt`

### Create Temporary Markdown File

```powershell
t md
```

Create and open an empty markdown file in the temporary folder.
It will be opened with the application associated with .txt files.

Example. `C:\tmp\yyyy-MM-dd\yyyy-MM-dd_HHmmss.md`


### Archive Old Temporary Folder

```powershell
t archive [--days <DAYS>]
```

Move temporary folders older than the specified number of days (\<DAYS>) to `.archive` folder.
\<DAYS> is default to 30.

### Cleanup Temporary Folder 

All commands perform a cleanup of the temporary folder before execution.
The cleanup removes 0-byte text files, markdown files, and empty folders.


