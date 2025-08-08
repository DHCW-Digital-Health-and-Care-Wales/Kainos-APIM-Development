# Secret Scanning

## Important Note

You **MUST** run the hooks and never omit them when pushing your changes to the repository. This ensures secrets are detected early, before they reach the GitHub repository.

Once secrets are pushed, simply removing them via interactive rebase is **not sufficient**. The correct procedure is:

- Rotate leaked secrets immediately.
- Remove commits from history so that commit hashes are no longer accessible in the GitHub repo.

---

## Install `git-secrets`

### macOS (Homebrew)

```bash
brew install git-secrets
```

### Manual Installation

```bash
git clone https://github.com/awslabs/git-secrets.git
cd git-secrets
make install
```

---

## Set Up `git-secrets` in Your Repository

```bash
cd your-repo/
git secrets --install
```

---

## Register AWS Patterns (Built-in)

```bash
git secrets --register-aws
```

---

## Add Custom Patterns

### Azure

```bash
git secrets --add 'azure_(client|tenant|subscription)_id'
git secrets --add 'DefaultEndpointsProtocol=https;AccountName=.*;AccountKey=.*;EndpointSuffix=core.windows.net'
git secrets --add 'SharedAccessSignature=.*'
```

### GCP

```bash
git secrets --add 'AIza[0-9A-Za-z\\-_]{35}'  # GCP API Key
git secrets --add '-----BEGIN PRIVATE KEY-----'  # GCP service account key
git secrets --add 'project_id:.*'
git secrets --add 'client_email:.*@.*\.iam\.gserviceaccount\.com'
```

---

## Usage

### Scan Staged Files Before Commit

```bash
git secrets --scan
```

### Scan Entire History (Recommended for Audits)

```bash
git secrets --scan-history
```

---

## Clean Secrets from History (If Needed)

Use tools like `git-filter-repo` or `BFG Repo-Cleaner`.

### Example with `git-filter-repo`

```bash
pip install git-filter-repo
git filter-repo --path yourfile --invert-paths
```

---

## Integration in CI/CD

Include the following in your pipeline to catch secrets early:

```bash
git secrets --scan
```

---

## References

- git-secrets GitHub Repository
