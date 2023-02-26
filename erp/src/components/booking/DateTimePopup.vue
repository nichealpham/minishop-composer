<template>
  <div>
    <v-btn
      outlined
      @click="showDialog = true"
      class="mt-1 mb-7 w-100 btn-fulltext-datetime-picker"
    >
      {{ fullText || $t("common.selectdatetime") }}
    </v-btn>
    <v-dialog v-model="showDialog" max-width="800px">
      <v-card class="pa-0 ma-0">
        <v-card-title>
          <span
            ><b>{{
              $t("common.select") + " " + $t("common.datetime")
            }}</b></span
          >
        </v-card-title>
        <v-card-text class="pa-0 ma-0 pl-5 pr-5">
          <v-container class="pa-0 ma-0 mt-2">
            <v-row>
              <v-col
                cols="12"
                sm="12"
                :md="!dateSelected ? 12 : 7"
                class="pa-0"
              >
                <v-date-picker
                  color="primary lighten-1"
                  header-color="white"
                  full-width
                  no-title
                  v-model="dateSelected"
                  flat
                ></v-date-picker>
              </v-col>
              <v-col cols="12" sm="12" md="5" v-show="dateSelected">
                <h3
                  class="black--text"
                  v-text="formatDateTime(dateSelected)"
                ></h3>
                <v-btn-toggle
                  color="primary"
                  borderless
                  mandatory
                  class="toggle_button_booking"
                  v-model="selectorType"
                  style="margin-top:-25px;float:right;"
                >
                  <v-btn small value="list" class="toggle_button_booking">
                    <v-icon small>mdi-format-list-bulleted </v-icon>
                  </v-btn>
                  <v-btn small value="clock" class="toggle_button_booking">
                    <v-icon small>mdi-clock-outline</v-icon>
                  </v-btn>
                </v-btn-toggle>
                <div class="mt-2" v-if="selectorType == 'clock'">
                  <div style="width:100%;display:block;text-align:center;">
                    <v-btn-toggle
                      borderless
                      mandatory
                      v-model="startEndType"
                      class="mt-1 mb-1"
                    >
                      <v-btn small value="start" class="toggle_button_booking">
                        {{ $t("common.timeStart") }}
                      </v-btn>
                      <v-btn small value="end" class="toggle_button_booking">
                        {{ $t("common.timeEnd") }}
                      </v-btn>
                    </v-btn-toggle>
                  </div>
                  <v-time-picker
                    v-show="startEndType == 'start'"
                    v-model="timeWheelStart"
                    format="24hr"
                  ></v-time-picker>
                  <v-time-picker
                    v-show="startEndType == 'end'"
                    v-model="timeWheelEnd"
                    format="24hr"
                  ></v-time-picker>
                  <v-btn
                    color="blue"
                    block
                    :disabled="disableTimeWheelConfirm"
                    elevation="0"
                    class="white--text"
                    @click="confirmTimeWheel()"
                  >
                    {{ $t("common.confirm") }}
                  </v-btn>
                </div>
                <div class="fit_time_picker mt-2" v-else>
                  <div
                    class="layout_btn"
                    v-for="time in timeOptions"
                    :key="time"
                  >
                    <v-btn
                      class="pa-4 mb-2"
                      outlined
                      color="indigo"
                      :width="timeSelected == time ? '48%' : '100%'"
                      @click="confirmTime(time)"
                    >
                      {{ time }}
                    </v-btn>
                    <v-btn
                      v-if="timeSelected == time"
                      class="ml-1 button_confirm"
                      @click="confirmDateTime(time)"
                      width="50%"
                      depressed
                      color="primary"
                    >
                      {{ $t("common.confirm") }}
                    </v-btn>
                  </div>
                </div>
              </v-col>
            </v-row>
          </v-container>
        </v-card-text>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import moment from "moment";

export default {
  components: {},
  props: {
    step: {
      type: Number,
      default: 60,
    },
    value: {
      type: Object,
      default() {
        return {
          timeStart: null,
          timeEnd: null,
        };
      },
    },
  },
  computed: {
    disableTimeWheelConfirm() {
      if (!this.timeWheelStart || !this.timeWheelEnd) {
        return true;
      }
      if (
        moment(`${this.dateSelected} ${this.timeWheelStart}`).isAfter(
          moment(`${this.dateSelected} ${this.timeWheelEnd}`)
        )
      ) {
        return true;
      }
      return false;
    },
  },
  data() {
    return {
      selectorType: "list",
      showDialog: false,
      dateSelected: null,
      timeSelected: null,
      startEndType: "start",
      timeWheelStart: null,
      timeWheelEnd: null,
      timeOptions: [],
      fullText: "",
    };
  },
  created() {
    this.renderTimeOptions();
    if (this.value) this.mapFullText(this.value);
  },
  methods: {
    confirmTime(time) {
      this.timeSelected = time;
    },
    confirmTimeWheel() {
      var timeStart = moment(this.dateSelected)
        .set({
          hour: this.timeWheelStart.split(":")[0],
          minute: this.timeWheelStart.split(":")[1],
        })
        .format();
      var timeEnd = moment(this.dateSelected)
        .set({
          hour: this.timeWheelEnd.split(":")[0],
          minute: this.timeWheelEnd.split(":")[1],
        })
        .format();
      this.mapFullText({ timeStart, timeEnd });
      this.$emit("input", { timeStart, timeEnd });
      this.showDialog = false;
    },
    confirmDateTime(timeStart) {
      timeStart = moment(this.dateSelected)
        .set({
          hour: timeStart.split(":")[0],
          minute: timeStart.split(":")[1],
        })
        .format();
      var timeEnd = moment(timeStart)
        .add(this.step, "minutes")
        .format();

      this.mapFullText({ timeStart, timeEnd });
      this.$emit("input", { timeStart, timeEnd });
      this.showDialog = false;
    },
    mapFullText({ timeStart, timeEnd }) {
      if (!timeStart || !timeEnd) {
        this.clean();
        return;
      }

      var dateSelected = moment(timeStart).format();
      timeStart = moment(timeStart).format("HH:mm");
      timeEnd = moment(timeEnd).format("HH:mm");

      var dateConfirm = moment(dateSelected).format("dddd DD/MM/YYYY ");
      this.fullText = `${timeStart} - ${timeEnd}, ${dateConfirm}`;
    },
    renderTimeOptions() {
      var timeStart = moment(moment("7:00", "HH:mm")).format("HH:mm");
      var timeEnd = moment(moment("21:00", "HH:mm")).format("HH:mm");

      while (timeStart <= timeEnd) {
        this.timeOptions.push(timeStart);
        timeStart = moment(timeStart, "HH:mm")
          .add(this.step, "minutes")
          .format("HH:mm");
      }
    },
    clean() {
      this.startEndType = "start";
      this.selectorType = "list";
      this.dateSelected = null;
      this.timeSelected = null;
      this.timeWheelStart = null;
      this.timeWheelEnd = null;
      this.fullText = "";
    },
    formatDateTime(val) {
      return moment(val).format("DD-MM-YYYY");
    },
  },
};
</script>

<style lang="scss" scoped>
.btn-fulltext-datetime-picker {
  text-transform: none;
  font-weight: 400;
}
.fit_time_picker {
  max-height: 300px;
  overflow-y: auto;
  overflow-x: hidden;
  -webkit-animation: anim 2.5s 1;
  animation: anim 2.5s 1;
  .layout_btn {
    width: 100%;
    display: flex;
    justify-content: center;
    .button_confirm {
      -webkit-animation: anim 2.5s 2;
      animation: anim 2.5s 2;
    }
  }
}

@-webkit-keyframes anim {
  0% {
    transform: translateX(100%);
  }
  14.28% {
    transform: translateX(0);
  }
  85.71% {
    transform: translateX(0);
  }
}
@keyframes anim {
  0% {
    transform: translateX(100%);
  }
  14.28% {
    transform: translateX(0);
  }
  85.71% {
    transform: translateX(0);
  }
}
</style>
