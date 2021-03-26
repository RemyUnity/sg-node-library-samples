
import { $, $hide, $show } from "../base/dom.ts";
import * as database from "./database.ts";
import * as treeBrowser from "./treeBrowser.ts";

window.addEventListener("error", () => $(".treeBrowser .title").classList.add("error"));

document.body.addEventListener("dragover", (event) => event.preventDefault());
document.body.addEventListener("drop", onDrop);

$(".setup .pickFolder button").addEventListener("click", onPickFolderClick);

(async function () {
  if (window.showDirectoryPicker == null) {
    $show(".setup .unsupported");
    $hide(".setup .supported");
    $show(".setup");
    return;
  }

  await database.open();
  const lastUsedFolder = await database.getLastUsedFolder();
  if (lastUsedFolder != null) {
    const status = await lastUsedFolder.handle.queryPermission({ mode: "read" });
    if (status === "granted") {
      openFolder(lastUsedFolder.handle);
    } else {
      $show(".setup .reopenLastUsedFolder");
      $(".setup .reopenLastUsedFolder button").addEventListener("click", () => openFolder(lastUsedFolder.handle));
    }
  }
  $show(".setup");
})();

async function openFolder(handle) {
  const status = await handle.requestPermission({ mode: "readwrite" });
  if (status !== "granted") return;
  $hide(".setup .reopenLastUsedFolder");

  await treeBrowser.build(handle);
  $show(".treeBrowser .toolbar");
  $show(".treeBrowser .tree");
  treeBrowser.focus();
}

async function onDrop(event) {
  event.preventDefault();
  if (event.dataTransfer.items.length !== 1) return;
  const item = event.dataTransfer.items[0];
  if (item.kind !== "file") return;
  const handle = await (item ).getAsFileSystemHandle();
  if (handle.kind !== "directory") return;

  await database.setLastUsedFolder(handle);
  openFolder(handle);
}

async function onPickFolderClick(event) {
  event.preventDefault();

  let handle;
  try { handle = await showDirectoryPicker(); }
  catch (e) { return; };

  await database.setLastUsedFolder(handle);
  openFolder(handle);
}
