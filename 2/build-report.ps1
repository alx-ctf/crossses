#Requires -Version 5.1
$ErrorActionPreference = 'Stop'

$root = $PSScriptRoot
$outPath = Join-Path $root 'Otchet_PZ2_Kobets_Kirill.docx'
$projectRoot = Join-Path $root 'BlogEfCore'
$textFile = Join-Path $root 'report-text-ru.txt'

$codeFiles = @(
    'Models\BlogPost.cs',
    'Models\Comment.cs',
    'Data\BlogDbContext.cs',
    'Data\BlogDbContextFactory.cs',
    'DbPath.cs',
    'Program.cs',
    'BlogEfCore.csproj',
    'Migrations\20260523080448_InitialCreate.cs'
)

function Add-Heading($s, [string]$t, [int]$lvl) {
    $styleId = -1 - $lvl
    $s.Style = $styleId
    $s.TypeText($t)
    $s.TypeParagraph()
    $s.Style = -1
}

function Add-Para($s, [string]$t) {
    if ([string]::IsNullOrWhiteSpace($t)) { $s.TypeParagraph(); return }
    $s.TypeText($t)
    $s.TypeParagraph()
}

function Add-Code($s, [string]$code) {
    $s.Font.Name = 'Consolas'
    $s.Font.Size = 9
    foreach ($line in ($code -split "`r?`n")) {
        $s.TypeText($line)
        $s.TypeParagraph()
    }
    $s.Font.Name = 'Times New Roman'
    $s.Font.Size = 12
}

$raw = Get-Content -Path $textFile -Raw -Encoding UTF8
$headerLines = New-Object System.Collections.Generic.List[string]
$sections = @()
$current = $null
foreach ($line in ($raw -split "`r?`n")) {
    if ($line -match '^---SECTION:([A-Z]+)(?::CODE)?---$') {
        if ($null -ne $current) { $sections += $current }
        $current = [pscustomobject]@{
            Key  = $Matches[1]
            Code = $line -match ':CODE---$'
            Title = ''
            Body = New-Object System.Collections.Generic.List[string]
        }
    }
    elseif ($line -eq '---' -and $null -ne $current -and $current.Title -eq '') {
        # skip separator after title line
    }
    elseif ($null -eq $current -and $line.Trim() -ne '' -and $line -ne '---') {
        [void]$headerLines.Add($line.Trim())
    }
    elseif ($null -ne $current) {
        if ($current.Title -eq '' -and $line.Trim() -ne '' -and $line -ne '---') {
            $current.Title = $line.Trim()
        }
        elseif ($line -ne '---') {
            [void]$current.Body.Add($line)
        }
    }
}
if ($null -ne $current) { $sections += $current }

$word = New-Object -ComObject Word.Application
$word.Visible = $false
$doc = $word.Documents.Add()
$sel = $word.Selection

if ($headerLines.Count -ge 1) { Add-Heading $sel $headerLines[0] 1 }
for ($h = 1; $h -lt $headerLines.Count; $h++) { Add-Para $sel $headerLines[$h] }
Add-Para $sel ('Date: ' + (Get-Date -Format 'dd.MM.yyyy'))
Add-Para $sel ''

foreach ($sec in $sections) {
    $title = if ($sec.Title) { $sec.Title } else { $sec.Key }
    Add-Heading $sel $title 2
    $body = ($sec.Body -join "`n").Trim()
    if ($sec.Code) { Add-Code $sel $body }
    else {
        foreach ($p in ($body -split "`n")) { Add-Para $sel $p.Trim() }
    }
    Add-Para $sel ''
}

foreach ($rel in $codeFiles) {
    $fp = Join-Path $projectRoot $rel
    if (-not (Test-Path $fp)) { continue }
    Add-Heading $sel $rel 3
    Add-Code $sel (Get-Content -Path $fp -Raw -Encoding UTF8).TrimEnd()
    Add-Para $sel ''
}

if (Test-Path $outPath) { Remove-Item $outPath -Force }
$savePath = [string]$outPath
$doc.SaveAs2($savePath) | Out-Null
$doc.Close()
$word.Quit()
Write-Host ('OK: ' + $outPath)
