---
description: Checkpoint before closing the IDE - verify build state, update the live DDR/roadmap with a resume point, confirm nothing is at risk
---

The user is about to close and reopen Visual Studio (or otherwise step away) and wants a safe checkpoint first. Do the following, then tell them when it's safe to close:

1. **Check working tree state.** Run `git status --short` and `git diff --stat`. Do NOT commit or push - only report what's uncommitted. All edits made via Read/Edit/Write tools are already saved to disk, not held in an editor buffer, so closing the IDE never loses work - reassure the user of this explicitly rather than assuming they know it.

2. **Verify the build is currently clean.** Run the project's normal build verification (see CLAUDE.md's `build` section for the exact commands - Release config, both bitness targets if applicable). Report errors/warnings if any exist; don't fix them unless asked, just record the true current state.

3. **Find the live in-progress DDR or plan, if any.** Look in `docs/decisions/` for the most recently modified DDR with an open/in-progress roadmap (e.g. an Epic-class DDR with unchecked milestones), or check for any other in-flight planning doc this session touched. If one exists and doesn't already have a current "Resume Point" (or equivalent) section reflecting *this* session's end state, add or update one with:
   - Exact current error/state counts if applicable (e.g. a scoping build's error count), with file:line specifics for anything unresolved
   - What was last verified and how (so a fresh session can re-verify quickly rather than re-derive)
   - The next concrete step and any open design question blocking it
   - Explicitly note what is NOT yet started, so scope isn't assumed incorrectly

   If no such document exists (this isn't mid-epic work), skip this step - don't manufacture a DDR for routine work.

4. **Check technical debt register** (`docs/debt.md`) - if anything closed out or newly identified this session isn't reflected there yet, update it.

5. **Persistent memory.** If this session's work is the kind of ongoing, multi-session project context that would help a *future* session with zero conversation history (not just this IDE restart) - update or add a project-type memory per the memory system's conventions. Skip this for routine single-session work; it's for genuinely multi-session efforts.

6. **Report back concisely**: what's saved, what's clean, what's open, and confirm it's safe to close. Do not pad this with restating things already obvious from the conversation.
