# 🐹 GuineaCS

**GuineaCS** is a lightweight, single-page web crawler written in C#.

This project was created as a **personal learning** journey — to better understand how web crawling works, from string parsing to link resolution, and eventually recursive crawling.

---

![Status](https://img.shields.io/badge/status-in%20development-yellow)
![Language](https://img.shields.io/badge/language-C%23-blue)
![License](https://img.shields.io/badge/license-MIT-green)

---

## 🚀 Features

- ✅ Crawl the web starting from a seed URL  
- ✅ Respect `robots.txt` compliance  
- ✅ Command-line flags for configuration  
- ✅ Filter links (e.g. `mailto:`, hashtags, domains)(Extensible through ILinkFilter.cs)  
- ✅ Export results to plain text or JSON  
- ✅ Save crawled HTML pages locally  
- (Coming soon): MongoDB / database export

## 🧾 Usage

```bash
dotnet run -url "https://example.com" -limit 25 -dmode truetrue
```
### CLI Flags
| Flag     | Description                                       | Example                      |
| -------- | ------------------------------------------------- | ---------------------------- |
| `-url`   | Seed URL to begin crawling                        | `-url "https://example.com"` |
| `-limit` | Number of pages to crawl                          | `-limit 25`                  |
| `-dmode` | Enable same-domain crawling only (`true`/`false`) | `-dmode true`                |

## 📚 Why "GuineaCS"?

Because guinea pigs are curious explorers — just like this crawler.  
And it’s written in C# — so I named it , **GuineaCS**.

---

## 💡 Goals

- Practice C# fundamentals
- Explore real-world software design
- Build a tool worth sharing

---

## 🛠 Specs

- Language: **C#**
- Runtime: **.NET 7+**
- Style: Minimal, modular, and educational

---

## 🙌 Contributing

GuineaCS is a personal project and a learning sandbox — but suggestions and ideas are always welcome.

---

## 👤 Author

Myself and my beautiful laptop.

---

## 📝 License

MIT License — free to use, share, and learn from.
