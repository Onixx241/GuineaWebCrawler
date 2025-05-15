# 🐹 GuineaCS

**GuineaCS** is a lightweight, single-page web crawler written in C#.

This project was created as a **personal learning** journey — to better understand how web crawling works, from string parsing to link resolution, and eventually recursive crawling.

---

![Status](https://img.shields.io/badge/status-in%20development-yellow)
![Language](https://img.shields.io/badge/language-C%23-blue)
![License](https://img.shields.io/badge/license-MIT-green)

---

## 🚀 Features

- ✅ Download and parse a single webpage
- ✅ Extract all `<a href="...">` links using regular expressions
- ✅ Normalize links (remove trailing slashes)
- ✅ Convert relative URLs to absolute
- ✅ Filter out unwanted link types (`mailto:`, `#`, etc.)
- ✅ Remove duplicates
- ✅ Save crawled HTML to a local file
- ✅ Print clean, readable results to console

---

## 🔧 Project Status

## ✅ Milestone: Phase 4 Complete

GuineaCS now has:
- Multi-page crawling using a breadth-first approach
- Queue-based link exploration
- Visited link tracking to avoid loops and repeats
- Basic link filtering (mailto:, `#`, etc.)
- Automatic saving of every visited page as numbered HTML files
- Input validation to skip malformed or unsupported links

You can now give it a single starting URL and it will explore an entire site layer by layer.

---

###  Next Milestone: Phase 5 - QOL Updates

| Feature                         | Status       |
|----------------------------------|--------------|
| Same-domain restriction          | 🔜 Working on now |
| Output log (all visited URLs)   | ✅ Done |
| CLI flags (`--url`, `--max-pages`, etc.) | 🔜 Planned |
| Crawl depth or page limit       | ✅ Done |
| Export to `.txt` or `.json`     | ✅ Done (.Json export on the way) |


---

## 📚 Why "GuineaCS"?

Because guinea pigs are curious explorers — just like this crawler.  
And it’s written in C# — so I aptly named it , **GuineaCS**.

---

## 💡 Goals

- Practice C# fundamentals
- Explore real-world software design
- Build a tool worth sharing and growing

---

## 🛠 Tech Stack

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
