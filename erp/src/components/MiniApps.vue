<template>
  <div>
    <div
      class="miniapp_window elevation-2"
      v-for="window in windows"
      :key="window.id"
      :id="window.id"
      :style="{
        width: window.width || '450px',
        maxWidth: window.maxWidth,
        maxHeight: window.maxHeight || 'none',
      }"
    >
      <div
        class="window_controller d-flex"
        :id="window.id + 'header'"
        @click="handleWindowClick(window.id)"
      >
        <span class="title">{{ window.name }}</span>
        <v-spacer />
        <div class="d-block">
          <v-badge
            bordered
            color="error"
            icon="mdi-close"
            offset-y="25"
            @click.native="removeWindow(window.id)"
          ></v-badge>
          <v-badge
            bordered
            color="warning"
            icon="mdi-arrow-top-right-bottom-left"
            offset-y="25"
            @click.native="openBrowserNewTab(window.url)"
          ></v-badge>
        </div>
      </div>
      <iframe :src="window.url" allow="camera; microphone; bluetooth"></iframe>
    </div>
    <div class="tool_bar" id="tool_bar">
      <v-tooltip top v-for="app in miniApps" :key="app.name">
        <template v-slot:activator="{ on, attrs }">
          <v-avatar
            class="elevation-2"
            :size="app.iconSize"
            tile
            v-bind="attrs"
            v-on="on"
            @click="addWindow(app)"
          >
            <img alt="Avatar" :src="app.icon" />
          </v-avatar>
        </template>
        <span>{{ app.name }}</span>
      </v-tooltip>
    </div>
  </div>
</template>

<script>
import { uuidv4, dragElement } from "@/plugins/helpers";
import { mapGetters } from "vuex";

export default {
  data() {
    return {
      miniApps: [
        {
          name: "ECG Visualizer",
          url: "https://ecg.phongkham.co",
          icon: "/icons/ecg.png",
          iconSize: 42,
          width: "calc(100% - 200px)",
          maxWidth: "1080px",
          maxHeight: "820px",
        },
        {
          name: "Medicine Reminder",
          url: "https://ivf-sandrasoft.web.app",
          icon: "https://ivf-sandrasoft.web.app/icons/book.png",
          iconSize: 42,
          maxHeight: "820px",
        },
        {
          name: "HolterVN",
          url: "https://ble-test.web.app",
          icon: "https://ble-test.web.app/icon.png",
          iconSize: 42,
          maxHeight: "820px",
        },
        {
          name: "Inner Me",
          url: "https://innerme.app",
          icon: "/icons/innerme.png",
          iconSize: 42,
          maxHeight: "820px",
        },
        {
          name: "Sandrasoft",
          url: "/",
          icon: "/icon.png",
          iconSize: 42,
          width: "calc(100% - 150px)",
          maxWidth: "1120px",
          maxHeight: "820px",
        },
      ],
      windows: [],
    };
  },
  computed: {
    ...mapGetters("MiniApp", ["openApp"]),
  },
  watch: {
    openApp(val) {
      if (val) {
        var { name, config } = val;
        if (!config) config = {};
        var app = this.miniApps.find((app) => app.name == name);
        if (!app) return;
        this.addWindow({ ...app, ...config });
      }
    },
  },
  created() {
    window.addEventListener("message", (e) => {
      // Get the sent data
      if (!e || !e.data) return;
      const data = JSON.parse(e.data);
      this.handleMiniAppEvent(data);
    });
  },
  methods: {
    async handleMiniAppEvent({ appName, eventName, data }) {
      this.$store.commit("MiniApp/CURRENT_EVENT", { appName, eventName, data });
    },
    async addWindow(app = {}) {
      var windowId = uuidv4();
      this.windows.push({
        id: windowId,
        ...app,
      });
      if (app && app.name == "Sandrasoft") {
        this.$store.commit("MiniApp/HIDE_MAIN_APP", true);
      }
      await this.sleep(200);
      this.handleWindowClick(windowId);
      dragElement(document.getElementById(windowId));
    },
    removeWindow(id) {
      var ind = this.windows.findIndex((w) => w.id == id);
      if (ind != -1) {
        var window = this.windows[ind];
        if (window.name == "Sandrasoft") {
          this.$store.commit("MiniApp/HIDE_MAIN_APP", false);
        }
        this.windows.splice(ind, 1);
      }
    },
    handleWindowClick(id) {
      var windows = document.getElementsByClassName("miniapp_window");
      windows.forEach((ele) => {
        ele.classList.remove("clicked");
      });
      document.getElementById(id).classList.add("clicked");
    },
  },
};
</script>

<style lang="scss">
.tool_bar {
  z-index: 100;
  display: inline-block;
  // bottom: 10px;
  left: 30px;
  // background-color: rgba(20, 20, 20, 0.8) !important;
  background-color: rgba(245, 245, 245, 0.45) !important;
  border-radius: 14px;
  padding: 10px 6px;
  box-shadow: 0px 0px 0px 0px rgb(0 0 0 / 20%), 0px 0px 0px 0px rgb(0 0 0 / 14%),
    0px 0px 0px 0px rgb(0 0 0 / 12%);
  position: absolute;
  // left: 50%; /* position the left edge of the element at the middle of the parent */
  // transform: translate(-50%);
  top: 50%; /* position the left edge of the element at the middle of the parent */
  transform: translate(-50%, -50%);
  width: 56px;
  transition: bottom 0.3s; /* Transition effect when sliding down (and up) */
  .v-avatar {
    // margin-right: 10px;
    margin-bottom: 10px;
    border-radius: 12px !important;
    transition: all 0.2s ease-in-out;
    opacity: 0.9;
  }
  .v-avatar:hover {
    transform: scale(1.1);
    opacity: 1;
  }
  .v-avatar:last-child {
    margin-right: 0px;
  }
}
.miniapp_window.clicked {
  z-index: 102;
}
.miniapp_window {
  min-width: 450px;
  height: calc(100vh - 100px);
  display: inline-block;
  position: absolute;
  left: 50%; /* position the left edge of the element at the middle of the parent */
  bottom: 50%;
  // transform: translate(-50%, calc(50% - 40px));
  transform: translate(-50%, calc(50% - 0px));
  background-color: rgba(245, 245, 245, 0.45) !important;
  overflow: hidden;
  border-radius: 24px !important;
  z-index: 101;
  border: 3px solid #6166f5;
  // border: 2px solid rgba(0, 0, 0, 1);
  // border: 2px solid rgba(245, 245, 245, 0.45);

  .window_controller {
    height: 30px;
    width: calc(100% + 10px);
    margin-left: -5px;
    position: relative;
    display: inline-block;
    background-color: #6166f5;
    padding-left: 25px;
    padding-right: 25px;
    cursor: move;
    .title {
      color: white;
      font-size: 1rem !important;
    }
    .v-badge {
      margin-right: 10px;
      float: right;
      min-width: 25px;
      min-height: 25px;
      cursor: pointer;
    }
  }
  iframe {
    width: calc(100% + 10px);
    margin-left: -5px;
    height: calc(100% - 20px);
    border: none;
    outline: none;
    margin-top: -10px;
  }
  $badge-bordered-width: 1px !important;
}
</style>
