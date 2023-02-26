<template>
  <div class="editor_wrapper mt-3">
    <div v-show="editable">
      <div class="mb-2 mt-3">
        <b style="font-size: 1rem">
          <v-icon class="mr-2" color="black">mdi-subtitles-outline </v-icon>
          {{ serviceName }}
          <v-btn
            @click="showMetadata = !showMetadata"
            fab
            icon
            x-small
            style="margin-top: -3px"
            class="ml-1"
            v-if="!$isPatient"
          >
            <v-icon>mdi-square-edit-outline</v-icon>
          </v-btn>
        </b>

        <div class="mt-2 mb-4" v-show="showMetadata">
          <b class="mb-1 ml-1">{{ $t("common.title") }}:</b>
          <v-text-field
            class="mb-2"
            v-model="serviceName"
            outlined
            dense
            hide-details
          />

          <b class="mb-1 ml-1">{{ $t("common.category") }}:</b>
          <div class="mt-0 mb-3">
            <category-selector
              :targetItemID="episode && episode.episodeID"
              :targetItemType="TargetItemType.Episode"
            />
          </div>

          <b class="mb-1 ml-1">{{ $t("common.galery") }}:</b>
          <div class="pl-2 pr-2 mt-1 mb-4">
            <image-galery
              :targetItemID="episode && episode.episodeID"
              :targetItemType="TargetItemType.Episode"
            />
          </div>

          <b class="mb-1 ml-1">{{ $t("common.headline") }}:</b>
          <text-editor v-model="serviceHeadline" />
        </div>
      </div>
      <div class="mb-2">
        <div class="mb-2">
          <v-icon class="mr-2" color="black">mdi-clipboard-text-outline</v-icon>
          <b class="ml-1 text-capitalize" style="font-size: 0.97rem"
            >{{ $t("common.examcontent") }}:</b
          >
        </div>
        <text-editor v-model="clinicalResult" />
        <div v-show="showDoctorNote" class="mb-2 mt-4">
          <v-icon class="mr-2" color="black">mdi-clipboard-file-outline</v-icon>
          <b class="ml-1 text-capitalize" style="font-size: 0.97rem"
            >{{ $t("common.clinicaldata") }}:</b
          >
        </div>
        <text-editor v-show="showDoctorNote" v-model="doctorNote" />
        <v-btn
          block
          max-height="32"
          :loading="loading"
          color="#6166f5"
          class="white--text mt-2"
          style="font-size: 0.8rem"
          @click="updateContent"
          v-show="showBtnSave"
        >
          <v-icon class="mr-2" small>mdi-content-save-outline</v-icon
          >{{ $t("common.save") }}</v-btn
        >
      </div>
    </div>
    <div v-show="!editable" class="editor_result_content">
      <b class="service_title" v-if="serviceName">{{ serviceName }}</b>
      <div v-if="serviceHeadline" v-html="serviceHeadline" class="mb-3"></div>
      <div v-if="clinicalResult">
        <!-- <div class="mb-1">
          <v-icon class="mr-2" color="black">mdi-clipboard-text-outline</v-icon>
          <b class="ml-1 text-capitalize" style="font-size:0.97rem;"
            >{{ $t("common.examcontent") }}:</b
          >
        </div> -->
        <div v-html="clinicalResult"></div>
      </div>
      <div v-else>
        <img class="img_book_cover" src="icons/book.png" />
        <p class="label_book_cover">
          {{ $t("episode.clinicaldatanotavailable") }}.
        </p>
      </div>
      <div v-if="doctorNote" class="mt-4">
        <div class="mb-1">
          <v-icon class="mr-2" color="black">mdi-clipboard-file-outline</v-icon>
          <b class="ml-1 text-capitalize" style="font-size: 0.97rem"
            >{{ $t("common.clinicaldata") }}:</b
          >
        </div>
        <div v-html="doctorNote"></div>
      </div>
      <div class="pl-15 pr-15 mt-3" v-if="noteTitle && noteMessage">
        <p class="note_title">{{ noteTitle }}</p>
        <p class="note">{{ noteMessage }}</p>
      </div>
    </div>
  </div>
</template>

<script>
import { EpisodeStatusType } from "@/plugins/contants";
import TextEditor from "@/components/ckeditor/TextEditor.vue";
import moment from "moment";
import ImageGalery from "@/components/ImageGalery.vue";
import { TargetItemType } from "@/plugins/contants";
import CategorySelector from "@/components/CategorySelector.vue";
import { mapGetters } from "vuex";

export default {
  components: {
    TextEditor,
    ImageGalery,
    CategorySelector,
  },
  props: ["episode"],
  watch: {
    episode: {
      deep: true,
      handler() {
        this.render();
      },
    },
    clinicalResult: {
      handler(val, old) {
        if (val != old) {
          this.showBtnSave = true;
          this.$emit("content-unsaved");
        }
      },
    },
    doctorNote: {
      handler(val, old) {
        if (val != old) {
          this.showBtnSave = true;
          this.$emit("content-unsaved");
        }
      },
    },
    serviceName: {
      handler(val, old) {
        if (val != old) {
          this.showBtnSave = true;
          this.$emit("content-unsaved");
        }
      },
    },
    serviceHeadline: {
      handler(val, old) {
        if (val != old) {
          this.showBtnSave = true;
          this.$emit("content-unsaved");
        }
      },
    },
    currentEvent(val) {
      if (!this.editable) return;
      this.handleMiniAppEvent(val);
    },
  },
  computed: {
    userID() {
      return this.$store.getters["Authen/getUser"].userID.toLowerCase();
    },
    ...mapGetters("MiniApp", ["currentEvent"]),
  },
  mounted() {
    this.render();
  },
  data() {
    return {
      TargetItemType,
      showMetadata: false,
      showDoctorNote: false,
      showBtnSave: false,
      editable: false,
      loading: false,
      recordID: "",
      clinicalResult: "",
      doctorNote: "",
      serviceName: "",
      serviceHeadline: "",
      noteTitle: "",
      noteMessage: "",
    };
  },
  methods: {
    render() {
      if (!this.episode) return;
      this.clean();
      var { records, statusID, userAdmittedID, publicTitle, publicHeadline } =
        this.episode;
      var record = (records && records[0]) || {};
      var { recordID, clinicalResult, doctorNote, userAppoint, service } =
        record;
      if (statusID == EpisodeStatusType.CheckedIn) {
        this.editable = true;
      }
      if (this.userID != userAdmittedID) {
        this.showDoctorNote = true;
      }

      this.recordID = recordID || "";
      this.clinicalResult = clinicalResult || "";
      this.doctorNote = doctorNote || "";
      this.serviceName = publicTitle || service.serviceName || "";
      this.serviceHeadline = publicHeadline || "";

      if (clinicalResult && !this.editable) {
        var { fullName, dateUpdated } = userAppoint;
        var dateStr = moment(dateUpdated).format("DD/MM/YYYY, h:mm:ss a");
        this.noteTitle = fullName;
        this.noteMessage = dateStr;
      }
    },
    handleMiniAppEvent({ appName, data }) {
      if (appName == "HolterVN") {
        var bodyDataPoint = "";
        data.datapoints.forEach((p) => {
          bodyDataPoint += `
          <tr>
            <td>${moment(p.date).format("HH:mm, DD-MM-YYYY")}</td>
            <td>${p.sys} - ${p.dia}</td>
            <td>${p.pulse}</td>
          </tr>`;
        });
        var linkUrl = `https://huyetap.phongkham.co/history/${data.id}`;
        this.clinicalResult += `
          <p><b>Kết quả đo huyết áp:</b></p>
          <figure class="table"><table>
            <tbody>
              <tr>
                <td><strong>Thời gian</strong></td>
                <td><strong>Huyết áp</strong></span></td>
                <td><strong>Nhịp tim</strong></span></td>
              </tr>
              ${bodyDataPoint}
            </tbody>
          </table></figure>
          <p><a href="${linkUrl}" class="ck-link_selected" target="_blank">
            <span>Xem tại HolterVN</span></a></p>`;
      }
      if (appName == "Inner Me") {
        var { voiceUrl, analytics } = data;
        var { text, analysis, expression } = analytics;
        this.clinicalResult += `
        <p><b>Kết quả nhận diện cảm xúc:</b></p>
        <ul>
          <li>Giọng nói: <a href="${voiceUrl}" target="_blank">Lắng nghe</a></li>
          <li>Nội dung chia sẻ: <b>${text}</b></li>
          <li>Cảm xúc qua giọng nói: <b>${analysis[2].emotion}</b></li>
          <li>Biểu cảm gương mặt: <b>${
            (expression && expression.primary) || "Normal"
          }</b></li>
          <li>Điểm hạnh phúc: <b>${
            analysis[0].documentSentiment.score * 100
          }/100</b></li>
        </ul>`;
      }
    },
    async updateContent() {
      if (!this.episode) return;
      var { statusID, clinicID, episodeID } = this.episode;
      if (statusID != EpisodeStatusType.CheckedIn) return;

      var clinicalResult = this.clinicalResult;
      var doctorNote = this.doctorNote;
      var publicTitle = this.serviceName || null;
      var publicHeadline = this.serviceHeadline || null;

      this.loading = true;
      await this.$httpClient.put(
        `/owner/clinics/${clinicID}/episodes/${episodeID}/records/${this.recordID}`,
        null,
        { clinicalResult, doctorNote, publicTitle, publicHeadline }
      );
      this.loading = false;
      // this.showSuccess("Content saved successfully.");

      this.showBtnSave = false;
      this.$emit("content-saved");
    },
    clean() {
      this.showMetadata = false;
      this.showDoctorNote = false;
      this.showBtnSave = false;
      this.editable = false;
      this.recordID = "";
      this.clinicalResult = "";
      this.doctorNote = "";
      this.serviceName = "";
      this.serviceHeadline = "";
      this.noteTitle = "";
      this.noteMessage = "";
    },
  },
};
</script>

<style lang="scss">
.service-headline-editor {
  .ck-editor__editable {
    min-height: 90px;
  }
}

.editor_wrapper {
  // .ck-editor__editable {
  //   min-height: calc(100vh - 230px);
  //   @media screen and (max-width: 819px) {
  //     min-height: calc(100vh - 285px);
  //   }
  // }
  .service_title {
    font-size: 1.1rem;
    text-align: center;
    width: 100%;
    display: inline-block;
  }
}
.editor_result_content {
  * {
    max-width: 100%;
    line-height: 1.65rem;
  }
  overflow-x: scroll;
  table {
    border-collapse: collapse;
    margin: 10px 0px;
  }
  table,
  th,
  td {
    border: 1px solid #757575;
    padding: 6px;
  }
  p {
    margin: 0px;
    padding: 0px;
  }
  figure.image {
    margin: 0 auto;
  }
  p.note {
    font-size: 0.85rem;
    opacity: 0.8;
    text-align: center;
    line-height: 1.5rem;
  }
  p.note_title {
    font-size: 0.95rem;
    text-align: center;
    line-height: 1.2rem;
  }
}
</style>
